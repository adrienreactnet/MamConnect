using System.Collections.Generic;
using System.Threading.Tasks;
using MamConnect.Application.Common.Interfaces;
using MamConnect.Domain.Entities;

namespace MamConnect.Application.Parents.Queries;

public class GetParentsQuery
{
    private readonly IUserRepository _userRepository;

    public GetParentsQuery(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// Retrieves all parents with their children.
    /// </summary>
    /// <returns>A read-only collection of parents.</returns>
    public async Task<IReadOnlyCollection<User>> ExecuteAsync()
    {
        IReadOnlyCollection<User> parents = await _userRepository.GetUsersWithChildrenByRoleAsync(UserRole.Parent);
        return parents;
    }
}
