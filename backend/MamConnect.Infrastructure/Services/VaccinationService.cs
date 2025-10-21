using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MamConnect.Domain.Entities;
using MamConnect.Domain.Services;
using MamConnect.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MamConnect.Infrastructure.Services;

public class VaccinationService : IVaccinationService
{
    private readonly AppDbContext dbContext;

    public VaccinationService(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Child?> GetChildScheduleAsync(int childId, CancellationToken cancellationToken)
    {
        Child? child = await dbContext.Children
            .Include(c => c.ChildVaccines)
            .ThenInclude(cv => cv.Vaccine)
            .FirstOrDefaultAsync(c => c.Id == childId, cancellationToken);

        if (child == null)
        {
            return null;
        }

        List<Vaccine> vaccines = await dbContext.Vaccines
            .OrderBy(v => v.Id)
            .ToListAsync(cancellationToken);

        bool newEntriesCreated = EnsureChildHasAllVaccines(child, vaccines);

        bool hasChanges = RecalculateStatuses(child.ChildVaccines);

        if (newEntriesCreated || hasChanges)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        return child;
    }

    public async Task<ChildVaccine?> UpdateChildVaccineAsync(
        int childId,
        int vaccineId,
        DateOnly? scheduledDate,
        DateOnly? administrationDate,
        VaccineStatus? status,
        string? comments,
        CancellationToken cancellationToken)
    {
        ChildVaccine? childVaccine = await dbContext.ChildVaccines
            .Include(cv => cv.Vaccine)
            .FirstOrDefaultAsync(cv => cv.ChildId == childId && cv.VaccineId == vaccineId, cancellationToken);

        if (childVaccine == null)
        {
            return null;
        }

        childVaccine.ScheduledDate = scheduledDate;
        childVaccine.AdministrationDate = administrationDate;
        if (status.HasValue)
        {
            childVaccine.Status = status.Value;
        }
        childVaccine.Comments = comments;
        childVaccine.UpdatedAt = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);
        await RecalculateChildStatusesAsync(childId, cancellationToken);

        return childVaccine;
    }

    public async Task<VaccinationOverview> GetOverviewAsync(CancellationToken cancellationToken)
    {
        List<Vaccine> vaccines = await dbContext.Vaccines
            .OrderBy(v => v.Id)
            .ToListAsync(cancellationToken);

        List<Child> children = await dbContext.Children
            .Include(c => c.ChildVaccines)
            .ToListAsync(cancellationToken);

        bool hasChanges = false;

        foreach (Child child in children)
        {
            bool newEntriesCreated = EnsureChildHasAllVaccines(child, vaccines);
            bool childChanged = RecalculateStatuses(child.ChildVaccines);
            if (newEntriesCreated || childChanged)
            {
                hasChanges = true;
            }
        }

        if (hasChanges)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        List<ChildVaccine> allVaccinations = children
            .SelectMany(c => c.ChildVaccines)
            .ToList();

        int totalChildren = children.Count;
        int totalVaccinations = allVaccinations.Count;
        int completedVaccinations = allVaccinations.Count(cv => cv.Status == VaccineStatus.Completed);
        int pendingVaccinations = allVaccinations.Count(cv => cv.Status == VaccineStatus.Pending);
        int toScheduleVaccinations = allVaccinations.Count(cv => cv.Status == VaccineStatus.ToSchedule);
        int overdueVaccinations = allVaccinations.Count(cv => cv.Status == VaccineStatus.Overdue);
        int childrenWithOverdueVaccinations = children.Count(c => c.ChildVaccines.Any(cv => cv.Status == VaccineStatus.Overdue));

        return new VaccinationOverview(
             totalChildren,
             totalVaccinations,
             completedVaccinations,
             pendingVaccinations,
             toScheduleVaccinations,
             overdueVaccinations,
             childrenWithOverdueVaccinations);
    }

    private async Task RecalculateChildStatusesAsync(int childId, CancellationToken cancellationToken)
    {
        Child? child = await dbContext.Children
            .Include(c => c.ChildVaccines)
            .ThenInclude(cv => cv.Vaccine)
            .FirstOrDefaultAsync(c => c.Id == childId, cancellationToken);

        if (child == null)
        {
            return;
        }

        bool hasChanges = RecalculateStatuses(child.ChildVaccines);

        if (hasChanges)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    private bool EnsureChildHasAllVaccines(Child child, IReadOnlyCollection<Vaccine> vaccines)
    {
        HashSet<int> existingVaccineIds = new HashSet<int>(child.ChildVaccines.Select(cv => cv.VaccineId));
        bool newEntriesCreated = false;
        DateTime now = DateTime.UtcNow;
        DateOnly today = DateOnly.FromDateTime(now);

        foreach (Vaccine vaccine in vaccines)
        {
            if (existingVaccineIds.Contains(vaccine.Id))
            {
                continue;
            }

            ChildVaccine childVaccine = new ChildVaccine
            {
                ChildId = child.Id,
                Child = child,
                VaccineId = vaccine.Id,
                Vaccine = vaccine,
                ScheduledDate = child.BirthDate.AddMonths(vaccine.AgeInMonths),
                CreatedAt = now
            };

            childVaccine.Status = GetComputedStatus(childVaccine, today);

            child.ChildVaccines.Add(childVaccine);
            dbContext.ChildVaccines.Add(childVaccine);
            newEntriesCreated = true;
        }

        return newEntriesCreated;
    }

    private bool RecalculateStatuses(IEnumerable<ChildVaccine> vaccinations)
    {
        bool hasChanges = false;
        DateOnly today = DateOnly.FromDateTime(DateTime.UtcNow);

        foreach (ChildVaccine vaccination in vaccinations)
        {
            VaccineStatus computedStatus = GetComputedStatus(vaccination, today);
            if (vaccination.Status != computedStatus)
            {
                vaccination.Status = computedStatus;
                vaccination.UpdatedAt = DateTime.UtcNow;
                hasChanges = true;
            }
        }

        return hasChanges;
    }

    private VaccineStatus GetComputedStatus(ChildVaccine vaccination, DateOnly today)
    {
        if (vaccination.AdministrationDate.HasValue)
        {
            return VaccineStatus.Completed;
        }

        if (!vaccination.ScheduledDate.HasValue)
        {
            return VaccineStatus.ToSchedule;
        }

        DateOnly scheduledDate = vaccination.ScheduledDate.Value;

        int dayOffset = scheduledDate.DayNumber - today.DayNumber;

        if (dayOffset > 30) // before 30 days
        {
            return VaccineStatus.Pending;
        }

        if (dayOffset >= 0) // Between 30 days and 0 day before
        {
            return VaccineStatus.ToSchedule;
        }        

        return VaccineStatus.Overdue;
    }
}
