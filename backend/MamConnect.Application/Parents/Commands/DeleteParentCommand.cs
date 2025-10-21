using System.Threading.Tasks;
using MamConnect.Application.Common.Interfaces;
using MamConnect.Domain.Entities;

namespace MamConnect.Application.Parents.Commands;

public class DeleteParentCommand
{
    private readonly IUserRepository _userRepository;

    public DeleteParentCommand(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// Deletes a parent.
    /// </summary>
    /// <param name="id">The parent identifier.</param>
    /// <returns><c>true</c> if the parent existed; otherwise <c>false</c>.</returns>
    public async Task<bool> ExecuteAsync(int id)
    {
        User? parent = await _userRepository.GetByIdAndRoleAsync(id, UserRole.Parent);
        if (parent == null)
        {
            return false;
        }

        await _userRepository.RemoveAsync(parent);
        await _userRepository.SaveChangesAsync();
        return true;
    }
}
