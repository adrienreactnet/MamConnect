using MamConnect.Api.Dtos;               // ChildRelationsDto
using MamConnect.Domain.Entities;        // Child
using MamConnect.Infrastructure.Data;    // AppDbContext
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MamConnect.Api.Controllers;

[Authorize]
[ApiController]
[Route("children")]
public class ChildrenController : ControllerBase
{
    private readonly AppDbContext _db;
    public ChildrenController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<IEnumerable<Child>> Get()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var role = User.FindFirstValue(ClaimTypes.Role);

        if (role == UserRole.Assistant.ToString())
        {
            return await _db.Children
                             .Where(c => c.AssistantId == userId)
                             .OrderBy(c => c.FirstName)
                             .ToListAsync();
        }

        if (role == UserRole.Parent.ToString())
        {
            return await _db.Children
                             .Where(c => c.Parents.Any(p => p.Id == userId))
                             .OrderBy(c => c.FirstName)
                             .ToListAsync();
        }

        if (role == UserRole.Admin.ToString())
        {
            return await _db.Children
                             .OrderBy(c => c.FirstName)
                             .ToListAsync();
        }

        return new List<Child>();
    }

    [HttpGet("with-relations")]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<IEnumerable<ChildRelationsDto>> GetWithRelations()
    {
        return await _db.Children
                         .Include(c => c.Assistant)
                         .Include(c => c.Parents)
                         .OrderBy(c => c.FirstName)
                         .Select(c => new ChildRelationsDto(
                             c.FirstName,
                             c.Assistant != null ? c.Assistant.FirstName + " " + c.Assistant.LastName : null,
                             c.Parents.Select(p => p.FirstName + " " + p.LastName).ToList()
                         ))
                         .ToListAsync();
    }

    [HttpPost]
    public async Task<IActionResult> Post(Child child)
    {
        _db.Add(child);
        await _db.SaveChangesAsync();
        return Created($"/children/{child.Id}", child);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Child input)
    {
        var child = await _db.Children.FindAsync(id);
        if (child is null) return NotFound();

        child.FirstName = input.FirstName;
        child.BirthDate = input.BirthDate;
        child.AssistantId = input.AssistantId;
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var child = await _db.Children.FindAsync(id);
        if (child is null) return NotFound();

        _db.Children.Remove(child);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
