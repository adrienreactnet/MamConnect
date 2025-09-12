namespace MamConnect.Api.Dtos;

using MamConnect.Domain.Entities;

public record CreateUserRequest(string Email, string FirstName, string LastName, string PhoneNumber, UserRole Role);
public record UserLoginRequest(string PhoneNumber, string Password);
public record AuthResponse(int Id, string FirstName, string LastName, string PhoneNumber, UserRole Role, string Token);