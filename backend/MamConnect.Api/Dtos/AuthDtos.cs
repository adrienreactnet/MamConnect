namespace MamConnect.Api.Dtos;

using MamConnect.Domain.Entities;

public record RegisterUserRequest(string Email, string Password);
public record UserLoginRequest(string Email, string Password);
public record AuthResponse(int Id, UserRole Role, string Token);