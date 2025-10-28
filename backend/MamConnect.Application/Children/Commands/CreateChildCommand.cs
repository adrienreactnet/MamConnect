using System.Threading;
using System.Threading.Tasks;
using MamConnect.Application.Common.Interfaces;
using MamConnect.Domain.Entities;
using MamConnect.Domain.Services;

namespace MamConnect.Application.Children.Commands;

public class CreateChildCommand
{
    private readonly IChildrenRepository _childrenRepository;
    private readonly IVaccinationService _vaccinationService;

    public CreateChildCommand(IChildrenRepository childrenRepository, IVaccinationService vaccinationService)
    {
        _childrenRepository = childrenRepository;
        _vaccinationService = vaccinationService;
    }

    /// <summary>
    /// Persists a new child in the repository.
    /// </summary>
    /// <param name="child">The child to create.</param>
    /// <param name="cancellationToken">Token allowing the operation to be cancelled.</param>
    /// <returns>The created child entity.</returns>
    public async Task<Child> ExecuteAsync(Child child, CancellationToken cancellationToken)
    {
        await _childrenRepository.AddAsync(child);
        await _childrenRepository.SaveChangesAsync();
        await _vaccinationService.EnsureChildVaccinesAsync(child.Id, cancellationToken);
        return child;
    }
}
