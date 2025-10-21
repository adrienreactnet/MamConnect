using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MamConnect.Api.Dtos;
using MamConnect.Application.Assistants.Commands;
using MamConnect.Application.Assistants.Queries;
using MamConnect.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MamConnect.Api.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("assistants")]
public class AssistantsController : ControllerBase
{
    private readonly GetAssistantsQuery _getAssistantsQuery;
    private readonly CreateAssistantCommand _createAssistantCommand;
    private readonly UpdateAssistantCommand _updateAssistantCommand;
    private readonly DeleteAssistantCommand _deleteAssistantCommand;

    public AssistantsController(
        GetAssistantsQuery getAssistantsQuery,
        CreateAssistantCommand createAssistantCommand,
        UpdateAssistantCommand updateAssistantCommand,
        DeleteAssistantCommand deleteAssistantCommand)
    {
        _getAssistantsQuery = getAssistantsQuery;
        _createAssistantCommand = createAssistantCommand;
        _updateAssistantCommand = updateAssistantCommand;
        _deleteAssistantCommand = deleteAssistantCommand;
    }

    [HttpGet]
    public async Task<IEnumerable<AssistantDto>> Get()
    {
        IReadOnlyCollection<User> assistants = await _getAssistantsQuery.ExecuteAsync();
        IEnumerable<AssistantDto> result = assistants.Select(ToDto);
        return result;
    }

    [HttpPost]
    public async Task<ActionResult<AssistantDto>> Post(AssistantDto dto)
    {
        User assistant = await _createAssistantCommand.ExecuteAsync(
            dto.Email,
            dto.FirstName,
            dto.LastName,
            dto.PhoneNumber);

        AssistantDto result = ToDto(assistant);
        return Created($"/assistants/{assistant.Id}", result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, AssistantDto input)
    {
        bool updated = await _updateAssistantCommand.ExecuteAsync(
            id,
            input.Email,
            input.FirstName,
            input.LastName,
            input.PhoneNumber);
        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        bool deleted = await _deleteAssistantCommand.ExecuteAsync(id);
        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }

    private static AssistantDto ToDto(User user)
    {
        AssistantDto dto = new AssistantDto(user.Id, user.Email, user.FirstName, user.LastName, user.PhoneNumber);
        return dto;
    }
}
