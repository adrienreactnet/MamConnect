using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MamConnect.Api.Dtos;
using MamConnect.Application.Vaccines.Commands;
using MamConnect.Application.Vaccines.Queries;
using MamConnect.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MamConnect.Api.Controllers;

[Authorize(Roles = nameof(UserRole.Admin))]
[ApiController]
[Route("vaccines")]
public class VaccinesController : ControllerBase
{
    private readonly GetVaccinesQuery _getVaccinesQuery;
    private readonly CreateVaccineCommand _createVaccineCommand;
    private readonly UpdateVaccineCommand _updateVaccineCommand;
    private readonly DeleteVaccineCommand _deleteVaccineCommand;

    public VaccinesController(
        GetVaccinesQuery getVaccinesQuery,
        CreateVaccineCommand createVaccineCommand,
        UpdateVaccineCommand updateVaccineCommand,
        DeleteVaccineCommand deleteVaccineCommand)
    {
        _getVaccinesQuery = getVaccinesQuery;
        _createVaccineCommand = createVaccineCommand;
        _updateVaccineCommand = updateVaccineCommand;
        _deleteVaccineCommand = deleteVaccineCommand;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<VaccineDto>>> Get()
    {
        IReadOnlyCollection<Vaccine> vaccines = await _getVaccinesQuery.ExecuteAsync();
        List<VaccineDto> result = vaccines
            .Select(vaccine => ToDto(vaccine))
            .ToList();

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<VaccineDto>> Post(VaccineDto input)
    {
        Vaccine vaccine = await _createVaccineCommand.ExecuteAsync(input.Name, input.AgeInMonths);
        VaccineDto dto = ToDto(vaccine);
        return Created($"/vaccines/{vaccine.Id}", dto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, VaccineDto input)
    {
        bool updated = await _updateVaccineCommand.ExecuteAsync(id, input.Name, input.AgeInMonths);
        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        bool deleted = await _deleteVaccineCommand.ExecuteAsync(id);
        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }

    private static VaccineDto ToDto(Vaccine vaccine)
    {
        return new VaccineDto(
            vaccine.Id,
            vaccine.Name,
            vaccine.AgeInMonths
        );
    }
}
