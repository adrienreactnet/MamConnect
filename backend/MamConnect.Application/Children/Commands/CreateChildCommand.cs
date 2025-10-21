using System.Threading.Tasks;
using MamConnect.Application.Common.Interfaces;
using MamConnect.Domain.Entities;

namespace MamConnect.Application.Children.Commands;

public class CreateChildCommand
{
    private readonly IChildrenRepository _childrenRepository;

    public CreateChildCommand(IChildrenRepository childrenRepository)
    {
        _childrenRepository = childrenRepository;
    }

    /// <summary>
    /// Persists a new child in the repository.
    /// </summary>
    /// <param name="child">The child to create.</param>
    /// <returns>The created child entity.</returns>
    public async Task<Child> ExecuteAsync(Child child)
    {
        await _childrenRepository.AddAsync(child);
        await _childrenRepository.SaveChangesAsync();
        return child;
    }
}
