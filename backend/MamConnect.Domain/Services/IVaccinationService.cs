using System;
using System.Threading;
using System.Threading.Tasks;
using MamConnect.Domain.Entities;

namespace MamConnect.Domain.Services;

public interface IVaccinationService
{
    Task<Child?> GetChildScheduleAsync(int childId, CancellationToken cancellationToken);

    Task<ChildVaccine?> UpdateChildVaccineAsync(
        int childId,
        int vaccineId,
        DateOnly? scheduledDate,
        DateOnly? administrationDate,
        VaccineStatus? status,
        string? comments,
        CancellationToken cancellationToken);

    Task<VaccinationOverview> GetOverviewAsync(CancellationToken cancellationToken);
}

public record VaccinationOverview(
    int TotalChildren,
    int TotalVaccinations,
    int CompletedVaccinations,
    int PendingVaccinations,
    int ToScheduleVaccinations,
    int OverdueVaccinations,
    int ChildrenWithOverdueVaccinations);