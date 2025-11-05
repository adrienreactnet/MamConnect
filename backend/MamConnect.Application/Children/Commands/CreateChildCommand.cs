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
    /// <returns>The result of the creation attempt.</returns>
    public async Task<Result> ExecuteAsync(Child child, CancellationToken cancellationToken)
    {
        string initialFirstName = child.FirstName ?? string.Empty;
        string initialLastName = child.LastName ?? string.Empty;
        string trimmedFirstName = initialFirstName.Trim();
        string trimmedLastName = initialLastName.Trim();
        child.FirstName = trimmedFirstName;
        child.LastName = trimmedLastName;
        string? trimmedAllergies = child.Allergies?.Trim();
        child.Allergies = string.IsNullOrWhiteSpace(trimmedAllergies) ? null : trimmedAllergies;

        bool exists = await _childrenRepository.ExistsWithFullNameAsync(trimmedFirstName, trimmedLastName);
        if (exists)
        {
            Result duplicateResult = new Result(ResultStatus.DuplicateFullName, null);
            return duplicateResult;
        }

        await _childrenRepository.AddAsync(child);
        await _childrenRepository.SaveChangesAsync();
        await _vaccinationService.EnsureChildVaccinesAsync(child.Id, cancellationToken);
        Result successResult = new Result(ResultStatus.Success, child);
        return successResult;
    }

    public enum ResultStatus
    {
        Success,
        DuplicateFullName
    }

    public sealed class Result
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class.
        /// </summary>
        /// <param name="status">The outcome of the creation attempt.</param>
        /// <param name="child">The created child when the operation succeeds.</param>
        public Result(ResultStatus status, Child? child)
        {
            Status = status;
            Child = child;
        }

        public ResultStatus Status { get; }

        public Child? Child { get; }
    }
}
