using System.Threading.Tasks;
using MamConnect.Application.Common.Interfaces;
using MamConnect.Application.Dtos;
using MamConnect.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace MamConnect.Application.Auth.Commands;

public class SetPasswordCommand
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IJwtTokenGenerator _tokenGenerator;

    public SetPasswordCommand(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IJwtTokenGenerator tokenGenerator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenGenerator = tokenGenerator;
    }

    public enum ResultStatus
    {
        Success,
        UserNotFound,
        AlreadyInitialized
    }

    /// <summary>
    /// Represents the result of setting a password for a user.
    /// </summary>
    public record Result(ResultStatus Status, AuthResponse? Response);

    /// <summary>
    /// Sets the initial password for a user who has not yet defined one.
    /// </summary>
    /// <param name="request">The password setup request.</param>
    /// <returns>A result describing the outcome of the operation.</returns>
    public async Task<Result> ExecuteAsync(SetPasswordRequest request)
    {
        User? user = await _userRepository.GetByPhoneNumberAsync(request.PhoneNumber);
        if (user == null)
        {
            return new Result(ResultStatus.UserNotFound, null);
        }

        if (!string.IsNullOrEmpty(user.PasswordHash))
        {
            return new Result(ResultStatus.AlreadyInitialized, null);
        }

        user.PasswordHash = _passwordHasher.HashPassword(user, request.NewPassword);
        await _userRepository.SaveChangesAsync();

        string token = _tokenGenerator.GenerateToken(user);
        AuthResponse response = new AuthResponse(user.Id, user.FirstName, user.LastName, user.PhoneNumber, user.Role, token);
        return new Result(ResultStatus.Success, response);
    }
}
