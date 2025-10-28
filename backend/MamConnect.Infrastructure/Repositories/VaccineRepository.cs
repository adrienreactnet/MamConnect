using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MamConnect.Application.Common.Interfaces;
using MamConnect.Domain.Entities;
using MamConnect.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MamConnect.Infrastructure.Repositories;

public class VaccineRepository : IVaccineRepository
{
    private readonly AppDbContext _dbContext;

    public VaccineRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<Vaccine>> GetAllAsync()
    {
        List<Vaccine> vaccines = await _dbContext.Vaccines
            .OrderBy(vaccine => vaccine.Name)
            .ThenBy(vaccine => vaccine.AgeInMonths)
            .ToListAsync();
        return vaccines;
    }

    public async Task<Vaccine?> FindByIdAsync(int id)
    {
        Vaccine? vaccine = await _dbContext.Vaccines.FindAsync(id);
        return vaccine;
    }

    public async Task AddAsync(Vaccine vaccine)
    {
        await _dbContext.Vaccines.AddAsync(vaccine);
    }

    public Task RemoveAsync(Vaccine vaccine)
    {
        _dbContext.Vaccines.Remove(vaccine);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
