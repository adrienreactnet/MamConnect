using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MamConnect.Infrastructure.Data;   // AppDbContext
using MamConnect.Domain.Entities;      // DailyReport

namespace MamConnect.Api.Controllers;

[ApiController]
[Route("children/{childId}/reports")]
public class DailyReportsController : ControllerBase
{
    private readonly AppDbContext _db;
    public DailyReportsController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<IEnumerable<DailyReport>> Get(int childId) =>
        await _db.DailyReports
                  .Where(r => r.ChildId == childId)
                  .OrderByDescending(r => r.CreatedAt)
                  .ToListAsync();

    public record DailyReportInput(string Content);

    [HttpPost]
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
        return Created($"/children/{childId}/reports/{report.Id}", report);
    }
}