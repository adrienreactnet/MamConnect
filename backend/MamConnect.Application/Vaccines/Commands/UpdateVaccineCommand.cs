using System.Threading.Tasks;
using MamConnect.Application.Common.Interfaces;
using MamConnect.Domain.Entities;

namespace MamConnect.Application.Vaccines.Commands;

public class UpdateVaccineCommand
{
    private readonly IVaccineRepository _vaccineRepository;

    public UpdateVaccineCommand(IVaccineRepository vaccineRepository)
    {
        _vaccineRepository = vaccineRepository;
    }

    /// <summary>
    /// Applies the provided values to an existing vaccine.
    /// </summary>
    /// <param name="id">The identifier of the vaccine to edit.</param>
    /// <param name="name">The new display name.</param>
    /// <param name="ageInMonths">The new recommended age in months.</param>
    /// <returns>True when the vaccine exists and has been updated.</returns>
    public async Task<bool> ExecuteAsync(int id, string name, int ageInMonths)
    {
        Vaccine? vaccine = await _vaccineRepository.FindByIdAsync(id);
        if (vaccine == null)
        {
            return false;
        }

        vaccine.Name = name;
        vaccine.AgeInMonths = ageInMonths;

        await _vaccineRepository.SaveChangesAsync();
        return true;
    }
}
