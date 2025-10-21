using System.Threading.Tasks;
using MamConnect.Application.Common.Interfaces;
using MamConnect.Application.Dtos;
using MamConnect.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace MamConnect.Application.Auth.Queries;

public class LoginUserQuery
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IJwtTokenGenerator _tokenGenerator;

    public LoginUserQuery(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IJwtTokenGenerator tokenGenerator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenGenerator = tokenGenerator;
    }

    /// <summary>
    /// Attempts to authenticate a user based on their credentials.
    /// </summary>
    /// <param name="request">The login request data.</param>
    /// <returns>The authentication response when successful; otherwise <c>null</c>.</returns>
    public async Task<AuthResponse?> ExecuteAsync(UserLoginRequest request)
    {
        User? user = await _userRepository.GetByPhoneNumberAsync(request.PhoneNumber);
        if (user == null)
        {
            return null;
        }

        PasswordVerificationResult verificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
        if (verificationResult == PasswordVerificationResult.Failed)
        {
            return null;
        }

        string token = _tokenGenerator.GenerateToken(user);
        AuthResponse response = new AuthResponse(user.Id, user.FirstName, user.LastName, user.PhoneNumber, user.Role, token);
        return response;
    }
}
