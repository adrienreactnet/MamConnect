using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MamConnect.Infrastructure.Data;   // AppDbContext
using MamConnect.Domain.Entities;      // DailyReport

namespace MamConnect.Api.Controllers;

[ApiController]
[Route("reports")]
public class DailyReportsController : ControllerBase
{
    private readonly AppDbContext _db;
    public DailyReportsController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<IEnumerable<DailyReport>> GetAll() =>
        await _db.DailyReports
                  .OrderByDescending(r => r.CreatedAt)
                  .ToListAsync();

    [HttpGet("children/{childId}")]
    public async Task<IEnumerable<DailyReport>> GetByChild(int childId) =>
        await _db.DailyReports
                  .Where(r => r.ChildId == childId)
                  .OrderByDescending(r => r.CreatedAt)
                  .ToListAsync();

    public record DailyReportInput(string Content);

    [HttpPost("children/{childId}")]
    public async Task<IActionResult> Post(int childId, DailyReportInput input)
    {
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
}