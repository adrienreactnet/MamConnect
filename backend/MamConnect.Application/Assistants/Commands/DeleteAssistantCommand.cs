using System.Threading.Tasks;
using MamConnect.Application.Common.Interfaces;
using MamConnect.Domain.Entities;

namespace MamConnect.Application.Assistants.Commands;

public class DeleteAssistantCommand
{
    private readonly IUserRepository _userRepository;

    public DeleteAssistantCommand(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// Deletes an assistant.
    /// </summary>
    /// <param name="id">The assistant identifier.</param>
    /// <returns><c>true</c> if the assistant existed; otherwise <c>false</c>.</returns>
    public async Task<bool> ExecuteAsync(int id)
    {
        User? assistant = await _userRepository.GetByIdAndRoleAsync(id, UserRole.Assistant);
        if (assistant == null)
        {
            return false;
        }

        await _userRepository.RemoveAsync(assistant);
        await _userRepository.SaveChangesAsync();
        return true;
    }
}
