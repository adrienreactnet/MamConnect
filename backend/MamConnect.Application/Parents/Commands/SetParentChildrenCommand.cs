using System.Collections.Generic;
using System.Threading.Tasks;
using MamConnect.Application.Common.Interfaces;
using MamConnect.Domain.Entities;

namespace MamConnect.Application.Parents.Commands;

public class SetParentChildrenCommand
{
    private readonly IUserRepository _userRepository;
    private readonly IChildrenRepository _childrenRepository;

    public SetParentChildrenCommand(IUserRepository userRepository, IChildrenRepository childrenRepository)
    {
        _userRepository = userRepository;
        _childrenRepository = childrenRepository;
    }

    /// <summary>
    /// Replaces the children assigned to a parent.
    /// </summary>
    /// <param name="parentId">The parent identifier.</param>
    /// <param name="childrenIds">The identifiers of the children to assign.</param>
    /// <returns><c>true</c> if the parent exists; otherwise <c>false</c>.</returns>
    public async Task<bool> ExecuteAsync(int parentId, IReadOnlyCollection<int> childrenIds)
    {
        User? parent = await _userRepository.GetByIdWithChildrenAsync(parentId, UserRole.Parent);
        if (parent == null)
        {
            return false;
        }

        IReadOnlyCollection<Child> children = await _childrenRepository.GetChildrenByIdsAsync(childrenIds);

        parent.Children.Clear();
        foreach (Child child in children)
        {
            parent.Children.Add(child);
        }

        await _userRepository.SaveChangesAsync();
        return true;
    }
}
