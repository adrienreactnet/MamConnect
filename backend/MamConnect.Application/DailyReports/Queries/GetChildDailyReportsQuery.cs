using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MamConnect.Application.Common.Interfaces;
using MamConnect.Domain.Entities;

namespace MamConnect.Application.DailyReports.Queries;

public class GetChildDailyReportsQuery
{
    private readonly GetAuthorizedChildIdsQuery _authorizedChildIdsQuery;
    private readonly IDailyReportRepository _dailyReportRepository;

    public GetChildDailyReportsQuery(GetAuthorizedChildIdsQuery authorizedChildIdsQuery, IDailyReportRepository dailyReportRepository)
    {
        _authorizedChildIdsQuery = authorizedChildIdsQuery;
        _dailyReportRepository = dailyReportRepository;
    }

    /// <summary>
    /// Represents the result of the child-specific daily report query.
    /// </summary>
    public record Result(bool IsAuthorized, IReadOnlyCollection<DailyReport> Reports);

    /// <summary>
    /// Retrieves the reports for a specific child when the user has access rights.
    /// </summary>
    /// <param name="userId">The identifier of the requesting user.</param>
    /// <param name="childId">The identifier of the child.</param>
    /// <returns>A result containing the authorization flag and the reports.</returns>
    public async Task<Result> ExecuteAsync(int userId, int childId)
    {
        IReadOnlyCollection<int> childIds = await _authorizedChildIdsQuery.ExecuteAsync(userId);
        if (!childIds.Contains(childId))
        {
            List<DailyReport> emptyReports = new List<DailyReport>();
            return new Result(false, emptyReports);
        }

        IReadOnlyCollection<DailyReport> reports = await _dailyReportRepository.GetReportsByChildIdAsync(childId);
        return new Result(true, reports);
    }
}
