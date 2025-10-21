using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MamConnect.Application.Common.Interfaces;
using MamConnect.Domain.Entities;

namespace MamConnect.Application.DailyReports.Queries;

public class GetAuthorizedChildIdsQuery
{
    private readonly IUserRepository _userRepository;

    public GetAuthorizedChildIdsQuery(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// Retrieves the identifiers of children accessible by the specified user.
    /// </summary>
    /// <param name="userId">The identifier of the requesting user.</param>
    /// <returns>A list of child identifiers accessible by the user.</returns>
    public async Task<IReadOnlyCollection<int>> ExecuteAsync(int userId)
    {
        User? user = await _userRepository.GetWithChildrenAsync(userId);
        if (user == null)
        {
            List<int> empty = new List<int>();
            return empty;
        }

        List<int> childIds = user.Children
            .Select(child => child.Id)
            .Concat(user.AssignedChildren.Select(child => child.Id))
            .Distinct()
            .ToList();
        return childIds;
    }
}
