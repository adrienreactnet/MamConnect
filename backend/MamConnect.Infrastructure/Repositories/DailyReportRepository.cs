using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MamConnect.Application.Common.Interfaces;
using MamConnect.Domain.Entities;
using MamConnect.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MamConnect.Infrastructure.Repositories;

public class DailyReportRepository : IDailyReportRepository
{
    private readonly AppDbContext _dbContext;

    public DailyReportRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<DailyReport>> GetReportsByChildIdsAsync(IReadOnlyCollection<int> childIds)
    {
        List<DailyReport> reports = await _dbContext.DailyReports
            .Where(report => childIds.Contains(report.ChildId))
            .OrderByDescending(report => report.CreatedAt)
            .ToListAsync();
        return reports;
    }

    public async Task<IReadOnlyCollection<DailyReport>> GetReportsByChildIdAsync(int childId)
    {
        List<DailyReport> reports = await _dbContext.DailyReports
            .Where(report => report.ChildId == childId)
            .OrderByDescending(report => report.CreatedAt)
            .ToListAsync();
        return reports;
    }

    public async Task AddAsync(DailyReport report)
    {
        await _dbContext.DailyReports.AddAsync(report);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
