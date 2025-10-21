using System.Threading.Tasks;
using MamConnect.Application.Common.Interfaces;
using MamConnect.Domain.Entities;

namespace MamConnect.Application.Children.Commands;

public class UpdateChildCommand
{
    private readonly IChildrenRepository _childrenRepository;

    public UpdateChildCommand(IChildrenRepository childrenRepository)
    {
        _childrenRepository = childrenRepository;
    }

    /// <summary>
    /// Updates an existing child with the provided information.
    /// </summary>
    /// <param name="id">The identifier of the child to update.</param>
    /// <param name="input">The new values for the child.</param>
    /// <returns><c>true</c> if the child exists and has been updated; otherwise <c>false</c>.</returns>
    public async Task<bool> ExecuteAsync(int id, Child input)
    {
        Child? child = await _childrenRepository.FindByIdAsync(id);
        if (child == null)
        {
            return false;
        }

        child.FirstName = input.FirstName;
        child.BirthDate = input.BirthDate;
        child.AssistantId = input.AssistantId;
        await _childrenRepository.SaveChangesAsync();
        return true;
    }
}
