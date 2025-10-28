using System.Threading.Tasks;
using MamConnect.Application.Common.Interfaces;
using MamConnect.Domain.Entities;

namespace MamConnect.Application.Vaccines.Commands;

public class CreateVaccineCommand
{
    private readonly IVaccineRepository _vaccineRepository;

    public CreateVaccineCommand(IVaccineRepository vaccineRepository)
    {
        _vaccineRepository = vaccineRepository;
    }

    /// <summary>
    /// Persists a new vaccine entry.
    /// </summary>
    /// <param name="name">The displayed name of the vaccine.</param>
    /// <param name="ageInMonths">The recommended age in months.</param>
    /// <returns>The created vaccine entity.</returns>
    public async Task<Vaccine> ExecuteAsync(string name, int ageInMonths)
    {
        Vaccine vaccine = new Vaccine
        {
            Name = name,
            AgeInMonths = ageInMonths
        };

        await _vaccineRepository.AddAsync(vaccine);
        await _vaccineRepository.SaveChangesAsync();
        return vaccine;
    }
}
