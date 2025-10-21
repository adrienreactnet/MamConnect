using System.Collections.Generic;
using System.Threading.Tasks;
using MamConnect.Application.Common.Interfaces;
using MamConnect.Domain.Entities;

namespace MamConnect.Application.Assistants.Queries;

public class GetAssistantsQuery
{
    private readonly IUserRepository _userRepository;

    public GetAssistantsQuery(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// Retrieves all assistants ordered by their names.
    /// </summary>
    /// <returns>A read-only collection of assistant users.</returns>
    public async Task<IReadOnlyCollection<User>> ExecuteAsync()
    {
        IReadOnlyCollection<User> assistants = await _userRepository.GetUsersByRoleAsync(UserRole.Assistant);
        return assistants;
    }
}
