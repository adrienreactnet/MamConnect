using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MamConnect.Api.Dtos;
using MamConnect.Application.Parents.Commands;
using MamConnect.Application.Parents.Queries;
using MamConnect.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MamConnect.Api.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("parents")]
public class ParentsController : ControllerBase
{
    private readonly GetParentsQuery _getParentsQuery;
    private readonly CreateParentCommand _createParentCommand;
    private readonly UpdateParentCommand _updateParentCommand;
    private readonly DeleteParentCommand _deleteParentCommand;
    private readonly SetParentChildrenCommand _setParentChildrenCommand;

    public ParentsController(
        GetParentsQuery getParentsQuery,
        CreateParentCommand createParentCommand,
        UpdateParentCommand updateParentCommand,
        DeleteParentCommand deleteParentCommand,
        SetParentChildrenCommand setParentChildrenCommand)
    {
        _getParentsQuery = getParentsQuery;
        _createParentCommand = createParentCommand;
        _updateParentCommand = updateParentCommand;
        _deleteParentCommand = deleteParentCommand;
        _setParentChildrenCommand = setParentChildrenCommand;
    }

    [HttpGet]
    public async Task<IEnumerable<ParentDto>> Get()
    {
        IReadOnlyCollection<User> parents = await _getParentsQuery.ExecuteAsync();
        IEnumerable<ParentDto> result = parents.Select(ToDto);
        return result;
    }

    [HttpPost]
    public async Task<ActionResult<ParentDto>> Post(ParentDto dto)
    {
        IReadOnlyCollection<int> childrenIds = dto.ChildrenIds?.ToArray() ?? Array.Empty<int>();
        User parent = await _createParentCommand.ExecuteAsync(
            dto.Email,
            dto.FirstName,
            dto.LastName,
            dto.PhoneNumber,
            childrenIds);

        ParentDto result = ToDto(parent);
        return Created($"/parents/{parent.Id}", result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, ParentDto input)
    {
        bool updated = await _updateParentCommand.ExecuteAsync(
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
        bool deleted = await _deleteParentCommand.ExecuteAsync(id);
        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPut("{id}/children")]
    public async Task<IActionResult> SetChildren(int id, int[] childIds)
    {
        IReadOnlyCollection<int> ids = childIds;
        bool updated = await _setParentChildrenCommand.ExecuteAsync(id, ids);
        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    private static ParentDto ToDto(User user)
    {
        IEnumerable<int> childrenIds = user.Children.Select(child => child.Id);
        ParentDto dto = new ParentDto(
            user.Id,
            user.Email,
            user.FirstName,
            user.LastName,
            user.PhoneNumber,
            childrenIds);
        return dto;
    }
}
