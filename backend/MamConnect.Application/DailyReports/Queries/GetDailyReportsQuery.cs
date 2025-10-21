using System.Collections.Generic;
using System.Threading.Tasks;
using MamConnect.Application.Common.Interfaces;
using MamConnect.Domain.Entities;

namespace MamConnect.Application.DailyReports.Queries;

public class GetDailyReportsQuery
{
    private readonly GetAuthorizedChildIdsQuery _authorizedChildIdsQuery;
    private readonly IDailyReportRepository _dailyReportRepository;

    public GetDailyReportsQuery(GetAuthorizedChildIdsQuery authorizedChildIdsQuery, IDailyReportRepository dailyReportRepository)
    {
        _authorizedChildIdsQuery = authorizedChildIdsQuery;
        _dailyReportRepository = dailyReportRepository;
    }

    /// <summary>
    /// Retrieves the daily reports accessible by the specified user.
    /// </summary>
    /// <param name="userId">The identifier of the requesting user.</param>
    /// <returns>A collection of daily reports visible to the user.</returns>
    public async Task<IReadOnlyCollection<DailyReport>> ExecuteAsync(int userId)
    {
        IReadOnlyCollection<int> childIds = await _authorizedChildIdsQuery.ExecuteAsync(userId);
        if (childIds.Count == 0)
        {
            List<DailyReport> emptyReports = new List<DailyReport>();
            return emptyReports;
        }

        return await _dailyReportRepository.GetReportsByChildIdsAsync(childIds);
    }
}
