using System.Collections.Generic;
using System.Threading.Tasks;
using MamConnect.Domain.Entities;

namespace MamConnect.Application.Common.Interfaces;

public interface IVaccineRepository
{
    Task<IReadOnlyCollection<Vaccine>> GetAllAsync();

    Task<Vaccine?> FindByIdAsync(int id);

    Task AddAsync(Vaccine vaccine);

    Task RemoveAsync(Vaccine vaccine);

    Task SaveChangesAsync();
}
