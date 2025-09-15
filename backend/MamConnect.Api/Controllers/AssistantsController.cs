using MamConnect.Api.Dtos;
using MamConnect.Domain.Entities;
using MamConnect.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MamConnect.Api.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("assistants")]
public class AssistantsController : ControllerBase
{
    private readonly AppDbContext _db;
    public AssistantsController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<IEnumerable<AssistantDto>> Get()
    {
        return await _db.Users
            .Where(u => u.Role == UserRole.Assistant)
            .OrderBy(u => u.FirstName)
            .Select(u => ToDto(u))
            .ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<AssistantDto>> Post(AssistantDto dto)
    {
        var user = new User
        {
            Email = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            PhoneNumber = dto.PhoneNumber,
            Role = UserRole.Assistant,
            PasswordHash = string.Empty
        };

        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        var result = ToDto(user);
        return Created($"/assistants/{user.Id}", result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, AssistantDto input)
    {
        var user = await _db.Users.SingleOrDefaultAsync(u => u.Id == id && u.Role == UserRole.Assistant);
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
        var user = await _db.Users.SingleOrDefaultAsync(u => u.Id == id && u.Role == UserRole.Assistant);
        if (user is null) return NotFound();

        _db.Users.Remove(user);
        await _db.SaveChangesAsync();
        return NoContent();
    }

    private static AssistantDto ToDto(User u) =>
        new(u.Id, u.Email, u.FirstName, u.LastName, u.PhoneNumber);
}