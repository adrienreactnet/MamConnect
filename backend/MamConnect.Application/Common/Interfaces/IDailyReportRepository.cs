using System.Collections.Generic;
using System.Threading.Tasks;
using MamConnect.Domain.Entities;

namespace MamConnect.Application.Common.Interfaces;

public interface IDailyReportRepository
{
    Task<IReadOnlyCollection<DailyReport>> GetReportsByChildIdsAsync(IReadOnlyCollection<int> childIds);
    Task<IReadOnlyCollection<DailyReport>> GetReportsByChildIdAsync(int childId);
    Task AddAsync(DailyReport report);
    Task SaveChangesAsync();
}
