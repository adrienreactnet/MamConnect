using System.Collections.Generic;
using System.Threading.Tasks;
using MamConnect.Application.Common.Interfaces;
using MamConnect.Domain.Entities;

namespace MamConnect.Application.Vaccines.Queries;

public class GetVaccinesQuery
{
    private readonly IVaccineRepository _vaccineRepository;

    public GetVaccinesQuery(IVaccineRepository vaccineRepository)
    {
        _vaccineRepository = vaccineRepository;
    }

    /// <summary>
    /// Retrieves the complete vaccine catalogue ordered by name then age.
    /// </summary>
    /// <returns>The ordered collection of vaccines.</returns>
    public async Task<IReadOnlyCollection<Vaccine>> ExecuteAsync()
    {
        IReadOnlyCollection<Vaccine> vaccines = await _vaccineRepository.GetAllAsync();
        return vaccines;
    }
}
