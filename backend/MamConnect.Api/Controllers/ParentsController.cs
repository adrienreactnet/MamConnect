using MamConnect.Api.Dtos;
using MamConnect.Domain.Entities;
using MamConnect.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MamConnect.Api.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("parents")]
public class ParentsController : ControllerBase
{
    private readonly AppDbContext _db;
    public ParentsController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<IEnumerable<ParentDto>> Get()
    {
        return await _db.Users
            .Where(u => u.Role == UserRole.Parent)
            .Include(u => u.Children)
            .OrderBy(u => u.FirstName)
            .Select(u => ToDto(u))
            .ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<ParentDto>> Post(ParentDto dto)
    {
        var user = new User
        {
            Email = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            PhoneNumber = dto.PhoneNumber,
            Role = UserRole.Parent,
            PasswordHash = string.Empty
        };

        if (dto.ChildrenIds?.Any() == true)
        {
            var children = await _db.Children
                .Where(c => dto.ChildrenIds.Contains(c.Id))
                .ToListAsync();
            foreach (var child in children)
            {
                user.Children.Add(child);
            }
        }

        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        var result = ToDto(user);
        return Created($"/parents/{user.Id}", result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, ParentDto input)
    {
        var user = await _db.Users.SingleOrDefaultAsync(u => u.Id == id && u.Role == UserRole.Parent);
        if (user is null) return NotFound();

        user.Email = input.Email;
        user.FirstName = input.FirstName;
        user.LastName = input.LastName;
        user.PhoneNumber = input.PhoneNumber;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _db.Users.SingleOrDefaultAsync(u => u.Id == id && u.Role == UserRole.Parent);
        if (user is null) return NotFound();

        _db.Users.Remove(user);
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpPut("{id}/children")]
    public async Task<IActionResult> SetChildren(int id, int[] childIds)
    {
        var user = await _db.Users
            .Include(u => u.Children)
            .SingleOrDefaultAsync(u => u.Id == id && u.Role == UserRole.Parent);
        if (user is null) return NotFound();

        var children = await _db.Children
            .Where(c => childIds.Contains(c.Id))
            .ToListAsync();

        user.Children.Clear();
        foreach (var child in children)
        {
            user.Children.Add(child);
        }

        await _db.SaveChangesAsync();
        return NoContent();
    }

    private static ParentDto ToDto(User u) =>
        new(
            u.Id,
            u.Email,
            u.FirstName,
            u.LastName,
            u.PhoneNumber,
            u.Children.Select(c => c.Id)
        );
}
