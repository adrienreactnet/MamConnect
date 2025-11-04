using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MamConnect.Api.Services;
using MamConnect.Application.DailyReports.Commands;
using MamConnect.Application.DailyReports.Queries;
using MamConnect.Application.Dtos;
using MamConnect.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MamConnect.Api.Controllers;

[Authorize]
[ApiController]
[Route("reports")]
public class DailyReportsController : ControllerBase
{
    private readonly GetDailyReportsQuery _getDailyReportsQuery;
    private readonly GetChildDailyReportsQuery _getChildDailyReportsQuery;
    private readonly CreateDailyReportCommand _createDailyReportCommand;
    private readonly ICurrentUserContext _currentUserContext;

    public DailyReportsController(
        GetDailyReportsQuery getDailyReportsQuery,
        GetChildDailyReportsQuery getChildDailyReportsQuery,
        CreateDailyReportCommand createDailyReportCommand,
        ICurrentUserContext currentUserContext)
    {
        _getDailyReportsQuery = getDailyReportsQuery;
        _getChildDailyReportsQuery = getChildDailyReportsQuery;
        _createDailyReportCommand = createDailyReportCommand;
        _currentUserContext = currentUserContext;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DailyReport>>> GetAll()
    {
        CurrentUser? currentUser;
        bool userAvailable = _currentUserContext.TryGetCurrentUser(out currentUser);
        if (!userAvailable || currentUser == null)
        {
            return Unauthorized();
        }

        IReadOnlyCollection<DailyReport> reports = await _getDailyReportsQuery.ExecuteAsync(currentUser.UserId);
        return Ok(reports);
    }

    [HttpGet("children/{childId}")]
    public async Task<ActionResult<IEnumerable<DailyReport>>> GetByChild(int childId)
    {
        CurrentUser? currentUser;
        bool userAvailable = _currentUserContext.TryGetCurrentUser(out currentUser);
        if (!userAvailable || currentUser == null)
        {
            return Unauthorized();
        }

        GetChildDailyReportsQuery.Result result = await _getChildDailyReportsQuery.ExecuteAsync(currentUser.UserId, childId);
        if (!result.IsAuthorized)
        {
            return Forbid();
        }

        return Ok(result.Reports);
    }

    [HttpPost("children/{childId}")]
    public async Task<IActionResult> Post(int childId, CreateDailyReportRequest input)
    {
        CurrentUser? currentUser;
        bool userAvailable = _currentUserContext.TryGetCurrentUser(out currentUser);
        if (!userAvailable || currentUser == null)
        {
            return Unauthorized();
        }

        CreateDailyReportCommand.Result result = await _createDailyReportCommand.ExecuteAsync(currentUser.UserId, childId, input);
        if (!result.IsAuthorized || result.Report == null)
        {
            return Forbid();
        }

        return Created($"/reports/{result.Report.Id}", result.Report);
    }
}
