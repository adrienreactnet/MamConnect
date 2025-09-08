using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MamConnect.Infrastructure.Data;   // AppDbContext
using MamConnect.Domain.Entities;      // Child

namespace MamConnect.Api.Controllers;

[ApiController]
[Route("children")]
public class ChildrenController : ControllerBase
{
    private readonly AppDbContext _db;
    public ChildrenController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<IEnumerable<Child>> Get() =>
        await _db.Children.OrderBy(c => c.FirstName).ToListAsync();

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
