using System.Threading.Tasks;
using MamConnect.Application.Common.Interfaces;
using MamConnect.Application.Dtos;
using MamConnect.Domain.Entities;

namespace MamConnect.Application.Auth.Commands;

public class RegisterUserCommand
{
    private readonly IUserRepository _userRepository;

    public RegisterUserCommand(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// Creates a new user account when the phone number is not already registered.
    /// </summary>
    /// <param name="request">The user creation request.</param>
    /// <returns><c>true</c> if the user has been created; otherwise <c>false</c>.</returns>
    public async Task<bool> ExecuteAsync(CreateUserRequest request)
    {
        bool exists = await _userRepository.ExistsWithPhoneNumberAsync(request.PhoneNumber);
        if (exists)
        {
            return false;
        }

        User user = new User
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber,
            Role = request.Role,
            PasswordHash = string.Empty
        };

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();
        return true;
    }
}
