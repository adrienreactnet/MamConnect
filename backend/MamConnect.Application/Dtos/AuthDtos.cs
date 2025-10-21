using MamConnect.Domain.Entities;

namespace MamConnect.Application.Dtos;

public record CreateUserRequest(string Email, string FirstName, string LastName, string PhoneNumber, UserRole Role);
public record UserLoginRequest(string PhoneNumber, string Password);
public record SetPasswordRequest(string PhoneNumber, string NewPassword);
public record AuthResponse(int Id, string FirstName, string LastName, string PhoneNumber, UserRole Role, string Token);
