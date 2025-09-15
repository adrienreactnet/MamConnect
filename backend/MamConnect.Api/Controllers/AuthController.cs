using MamConnect.Api.Dtos;
using MamConnect.Domain.Entities;
using MamConnect.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MamConnect.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IConfiguration _config;
    private readonly IPasswordHasher<User> _passwordHasher;

    public AuthController(AppDbContext db, IConfiguration config, IPasswordHasher<User> passwordHasher)
    {
        _db = db;
        _config = config;
        _passwordHasher = passwordHasher;
    }

    [Authorize]
    [HttpPost("register")]
    public async Task<IActionResult> Register(CreateUserRequest request)
    {
        if (await _db.Users.AnyAsync(u => u.PhoneNumber == request.PhoneNumber))
            return Conflict();

        var user = new User
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber,
            Role = request.Role,
            PasswordHash = string.Empty
        };

        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(UserLoginRequest request)
    {
        User? user = await _db.Users.SingleOrDefaultAsync(u => u.PhoneNumber == request.PhoneNumber);
        if (user is null)
            return Unauthorized();

        PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
        if (result == PasswordVerificationResult.Failed)
            return Unauthorized();

        string token = GenerateToken(user);
        return new AuthResponse(user.Id, user.FirstName, user.LastName, user.PhoneNumber, user.Role, token);
    }

    [AllowAnonymous]
    [HttpPost("set-password")]
    public async Task<ActionResult<AuthResponse>> SetPassword(SetPasswordRequest request)
    {
        var user = await _db.Users.SingleOrDefaultAsync(u => u.PhoneNumber == request.PhoneNumber);
        if (user is null)
            return Unauthorized();

        if (!string.IsNullOrEmpty(user.PasswordHash))
            return Conflict();

        user.PasswordHash = _passwordHasher.HashPassword(user, request.NewPassword);
        await _db.SaveChangesAsync();

        var token = GenerateToken(user);
        return new AuthResponse(user.Id, user.FirstName, user.LastName, user.PhoneNumber, user.Role, token);
    }

    private string GenerateToken(User user)
    {
        var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]!);
        var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}