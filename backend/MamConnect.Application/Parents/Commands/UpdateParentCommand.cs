using System.Threading.Tasks;
using MamConnect.Application.Common.Interfaces;
using MamConnect.Domain.Entities;

namespace MamConnect.Application.Parents.Commands;

public class UpdateParentCommand
{
    private readonly IUserRepository _userRepository;

    public UpdateParentCommand(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// Updates the profile information of a parent.
    /// </summary>
    /// <param name="id">The parent identifier.</param>
    /// <param name="email">The updated email.</param>
    /// <param name="firstName">The updated first name.</param>
    /// <param name="lastName">The updated last name.</param>
    /// <param name="phoneNumber">The updated phone number.</param>
    /// <returns><c>true</c> if the parent exists; otherwise <c>false</c>.</returns>
    public async Task<bool> ExecuteAsync(int id, string email, string firstName, string lastName, string phoneNumber)
    {
        User? parent = await _userRepository.GetByIdAndRoleAsync(id, UserRole.Parent);
        if (parent == null)
        {
            return false;
        }

        parent.Email = email;
        parent.FirstName = firstName;
        parent.LastName = lastName;
        parent.PhoneNumber = phoneNumber;

        await _userRepository.SaveChangesAsync();
        return true;
    }
}
