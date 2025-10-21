using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
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

    public DailyReportsController(
        GetDailyReportsQuery getDailyReportsQuery,
        GetChildDailyReportsQuery getChildDailyReportsQuery,
        CreateDailyReportCommand createDailyReportCommand)
    {
        _getDailyReportsQuery = getDailyReportsQuery;
        _getChildDailyReportsQuery = getChildDailyReportsQuery;
        _createDailyReportCommand = createDailyReportCommand;
    }

    [HttpGet]
    public async Task<IEnumerable<DailyReport>> GetAll()
    {
        string? userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userIdValue == null)
        {
            List<DailyReport> emptyReports = new List<DailyReport>();
            return emptyReports;
        }

        int userId = int.Parse(userIdValue);
        IReadOnlyCollection<DailyReport> reports = await _getDailyReportsQuery.ExecuteAsync(userId);
        return reports;
    }

    [HttpGet("children/{childId}")]
    public async Task<ActionResult<IEnumerable<DailyReport>>> GetByChild(int childId)
    {
        string? userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userIdValue == null)
        {
            return Forbid();
        }

        int userId = int.Parse(userIdValue);
        GetChildDailyReportsQuery.Result result = await _getChildDailyReportsQuery.ExecuteAsync(userId, childId);
        if (!result.IsAuthorized)
        {
            return Forbid();
        }

        return Ok(result.Reports);
    }

    [HttpPost("children/{childId}")]
    public async Task<IActionResult> Post(int childId, CreateDailyReportRequest input)
    {
        string? userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userIdValue == null)
        {
            return Forbid();
        }

        int userId = int.Parse(userIdValue);
        CreateDailyReportCommand.Result result = await _createDailyReportCommand.ExecuteAsync(userId, childId, input);
        if (!result.IsAuthorized || result.Report == null)
        {
            return Forbid();
        }

        return Created($"/reports/{result.Report.Id}", result.Report);
    }
}
