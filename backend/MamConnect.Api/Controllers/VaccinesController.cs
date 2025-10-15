using MamConnect.Api.Dtos;
using MamConnect.Domain.Entities;
using MamConnect.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;

namespace MamConnect.Api.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/vaccines")]
public class VaccinesController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public VaccinesController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<VaccineDto>>> Get()
    {
        List<Vaccine> vaccines = await _dbContext.Vaccines
            .OrderBy(vaccine => vaccine.Name)
            .ToListAsync();

        List<VaccineDto> result = vaccines
            .Select(vaccine => ToDto(vaccine))
            .ToList();

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<VaccineDto>> Post(VaccineDto input)
    {
        Vaccine vaccine = new Vaccine
        {
            Name = input.Name,
            AgesInMonths = input.AgesInMonths
        };

        _dbContext.Vaccines.Add(vaccine);
        await _dbContext.SaveChangesAsync();

        VaccineDto dto = ToDto(vaccine);
        return Created($"/api/vaccines/{vaccine.Id}", dto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, VaccineDto input)
    {
        ValueTask<Vaccine?> findTask = _dbContext.Vaccines.FindAsync(id);
        Vaccine? vaccine = await findTask;

        if (vaccine == null)
        {
            return NotFound();
        }

        vaccine.Name = input.Name;
        vaccine.AgesInMonths = input.AgesInMonths;

        await _dbContext.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        ValueTask<Vaccine?> findTask = _dbContext.Vaccines.FindAsync(id);
        Vaccine? vaccine = await findTask;

        if (vaccine == null)
        {
            return NotFound();
        }

        _dbContext.Vaccines.Remove(vaccine);
        await _dbContext.SaveChangesAsync();
        return NoContent();
    }

    private static VaccineDto ToDto(Vaccine vaccine)
    {
        return new VaccineDto(
            vaccine.Id,
            vaccine.Name,
            vaccine.AgesInMonths
        );
    }
}