using System.Threading.Tasks;
using MamConnect.Application.Common.Interfaces;
using MamConnect.Domain.Entities;

namespace MamConnect.Application.Assistants.Commands;

public class CreateAssistantCommand
{
    private readonly IUserRepository _userRepository;

    public CreateAssistantCommand(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// Creates a new assistant account.
    /// </summary>
    /// <param name="email">The assistant email.</param>
    /// <param name="firstName">The assistant first name.</param>
    /// <param name="lastName">The assistant last name.</param>
    /// <param name="phoneNumber">The assistant phone number.</param>
    /// <returns>The created assistant.</returns>
    public async Task<User> ExecuteAsync(string email, string firstName, string lastName, string phoneNumber)
    {
        User assistant = new User
        {
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = phoneNumber,
            Role = UserRole.Assistant,
            PasswordHash = string.Empty
        };

        await _userRepository.AddAsync(assistant);
        await _userRepository.SaveChangesAsync();
        return assistant;
    }
}
