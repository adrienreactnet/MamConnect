using System.Threading.Tasks;
using MamConnect.Application.Common.Interfaces;
using MamConnect.Domain.Entities;

namespace MamConnect.Application.Vaccines.Commands;

public class DeleteVaccineCommand
{
    private readonly IVaccineRepository _vaccineRepository;

    public DeleteVaccineCommand(IVaccineRepository vaccineRepository)
    {
        _vaccineRepository = vaccineRepository;
    }

    /// <summary>
    /// Removes the requested vaccine if present.
    /// </summary>
    /// <param name="id">The identifier of the vaccine to delete.</param>
    /// <returns>True when the record existed and has been removed.</returns>
    public async Task<bool> ExecuteAsync(int id)
    {
        Vaccine? vaccine = await _vaccineRepository.FindByIdAsync(id);
        if (vaccine == null)
        {
            return false;
        }

        await _vaccineRepository.RemoveAsync(vaccine);
        await _vaccineRepository.SaveChangesAsync();
        return true;
    }
}
