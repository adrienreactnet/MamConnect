using System.Threading.Tasks;
using MamConnect.Application.Common.Interfaces;
using MamConnect.Domain.Entities;

namespace MamConnect.Application.Assistants.Commands;

public class UpdateAssistantCommand
{
    private readonly IUserRepository _userRepository;

    public UpdateAssistantCommand(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// Updates an existing assistant.
    /// </summary>
    /// <param name="id">The assistant identifier.</param>
    /// <param name="email">The updated email.</param>
    /// <param name="firstName">The updated first name.</param>
    /// <param name="lastName">The updated last name.</param>
    /// <param name="phoneNumber">The updated phone number.</param>
    /// <returns><c>true</c> if the assistant exists; otherwise <c>false</c>.</returns>
    public async Task<bool> ExecuteAsync(int id, string email, string firstName, string lastName, string phoneNumber)
    {
        User? assistant = await _userRepository.GetByIdAndRoleAsync(id, UserRole.Assistant);
        if (assistant == null)
        {
            return false;
        }

        assistant.Email = email;
        assistant.FirstName = firstName;
        assistant.LastName = lastName;
        assistant.PhoneNumber = phoneNumber;

        await _userRepository.SaveChangesAsync();
        return true;
    }
}
