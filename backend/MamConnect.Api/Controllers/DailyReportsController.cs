using MamConnect.Domain.Entities;
using MamConnect.Infrastructure.Data;   // AppDbContext
using Microsoft.AspNetCore.Authorization;      // DailyReport
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;

namespace MamConnect.Api.Controllers;

[Authorize]
[ApiController]
[Route("reports")]
public class DailyReportsController : ControllerBase
{
    private readonly AppDbContext _db;
    public DailyReportsController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<IEnumerable<DailyReport>> GetAll()
    {
        var authorizedIds = await GetAuthorizedChildIds();
        return await _db.DailyReports
                        .Where(r => authorizedIds.Contains(r.ChildId))
                        .OrderByDescending(r => r.CreatedAt)
                        .ToListAsync();
    }

    [HttpGet("children/{childId}")]
    public async Task<ActionResult<IEnumerable<DailyReport>>> GetByChild(int childId)
    {
        var authorizedIds = await GetAuthorizedChildIds();
        if (!authorizedIds.Contains(childId))
            return Forbid();

        var reports = await _db.DailyReports
                               .Where(r => r.ChildId == childId)
                               .OrderByDescending(r => r.CreatedAt)
                               .ToListAsync();
        return reports;
    }

    public record DailyReportInput(string Content);

    [HttpPost("children/{childId}")]
    public async Task<IActionResult> Post(int childId, DailyReportInput input)
    {
        var authorizedIds = await GetAuthorizedChildIds();
        if (!authorizedIds.Contains(childId))
            return Forbid();

        var report = new DailyReport
        {
            ChildId = childId,
            Content = input.Content,
            CreatedAt = DateTime.UtcNow
        };
        _db.Add(report);
        await _db.SaveChangesAsync();
        return Created($"/reports/{report.Id}", report);
    }

    private async Task<List<int>> GetAuthorizedChildIds()
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userIdClaim is null)
            return new();

        var userId = int.Parse(userIdClaim);
        var user = await _db.Users
                            .Include(u => u.Children)
                            .Include(u => u.AssignedChildren)
                            .SingleOrDefaultAsync(u => u.Id == userId);

        return user is null
            ? new()
            : user.Children.Select(c => c.Id)
                   .Concat(user.AssignedChildren.Select(c => c.Id))
                   .ToList();
    }
}