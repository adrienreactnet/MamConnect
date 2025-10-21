using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MamConnect.Application.Common.Interfaces;
using MamConnect.Application.Dtos;
using MamConnect.Domain.Entities;

namespace MamConnect.Application.DailyReports.Commands;

public class CreateDailyReportCommand
{
    private readonly GetAuthorizedChildIdsQuery _authorizedChildIdsQuery;
    private readonly IDailyReportRepository _dailyReportRepository;

    public CreateDailyReportCommand(GetAuthorizedChildIdsQuery authorizedChildIdsQuery, IDailyReportRepository dailyReportRepository)
    {
        _authorizedChildIdsQuery = authorizedChildIdsQuery;
        _dailyReportRepository = dailyReportRepository;
    }

    /// <summary>
    /// Represents the result of a daily report creation attempt.
    /// </summary>
    public record Result(bool IsAuthorized, DailyReport? Report);

    /// <summary>
    /// Creates a new daily report when the user is authorized for the child.
    /// </summary>
    /// <param name="userId">The identifier of the requesting user.</param>
    /// <param name="childId">The identifier of the child.</param>
    /// <param name="request">The report creation data.</param>
    /// <returns>The result describing authorization status and the created report.</returns>
    public async Task<Result> ExecuteAsync(int userId, int childId, CreateDailyReportRequest request)
    {
        IReadOnlyCollection<int> childIds = await _authorizedChildIdsQuery.ExecuteAsync(userId);
        if (!childIds.Contains(childId))
        {
            return new Result(false, null);
        }

        DailyReport report = new DailyReport
        {
            ChildId = childId,
            Content = request.Content,
            CreatedAt = DateTime.UtcNow
        };

        await _dailyReportRepository.AddAsync(report);
        await _dailyReportRepository.SaveChangesAsync();

        return new Result(true, report);
    }
}
