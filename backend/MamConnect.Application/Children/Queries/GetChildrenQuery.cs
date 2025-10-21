using System.Collections.Generic;
using System.Threading.Tasks;
using MamConnect.Application.Common.Interfaces;
using MamConnect.Domain.Entities;

namespace MamConnect.Application.Children.Queries;

public class GetChildrenQuery
{
    private readonly IChildrenRepository _childrenRepository;

    public GetChildrenQuery(IChildrenRepository childrenRepository)
    {
        _childrenRepository = childrenRepository;
    }

    /// <summary>
    /// Retrieves the children accessible by the specified user based on their role.
    /// </summary>
    /// <param name="userId">The identifier of the requesting user.</param>
    /// <param name="role">The role of the requesting user.</param>
    /// <returns>A read-only collection of children matching the access rules.</returns>
    public async Task<IReadOnlyCollection<Child>> ExecuteAsync(int userId, UserRole role)
    {
        if (role == UserRole.Assistant)
        {
            return await _childrenRepository.GetChildrenByAssistantIdAsync(userId);
        }

        if (role == UserRole.Parent)
        {
            return await _childrenRepository.GetChildrenByParentIdAsync(userId);
        }

        if (role == UserRole.Admin)
        {
            return await _childrenRepository.GetAllChildrenAsync();
        }

        List<Child> children = new List<Child>();
        return children;
    }
}
