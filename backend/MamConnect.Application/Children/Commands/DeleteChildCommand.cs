using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MamConnect.Application.Common.Interfaces;
using MamConnect.Domain.Entities;

namespace MamConnect.Application.Children.Commands;

public class DeleteChildCommand
{
    private readonly IChildrenRepository _childrenRepository;
    private readonly IUserRepository _userRepository;

    public DeleteChildCommand(IChildrenRepository childrenRepository, IUserRepository userRepository)
    {
        _childrenRepository = childrenRepository;
        _userRepository = userRepository;
    }

    /// <summary>
    /// Deletes a child and cleans up orphan parent accounts when necessary.
    /// </summary>
    /// <param name="id">The identifier of the child to delete.</param>
    /// <returns><c>true</c> if the child existed and has been deleted; otherwise <c>false</c>.</returns>
    public async Task<bool> ExecuteAsync(int id)
    {
        Child? child = await _childrenRepository.GetChildWithParentsAsync(id);
        if (child == null)
        {
            return false;
        }

        List<User> parentsToRemove = child.Parents
            .Where(parent => parent.Children.Count == 1)
            .ToList();

        await _childrenRepository.RemoveAsync(child);

        foreach (User parent in parentsToRemove)
        {
            await _userRepository.RemoveAsync(parent);
        }

        await _childrenRepository.SaveChangesAsync();
        return true;
    }
}
