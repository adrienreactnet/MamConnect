using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MamConnect.Api.Dtos;
using MamConnect.Api.Mappings;
using MamConnect.Api.Services;
using MamConnect.Application.Children.Commands;
using MamConnect.Application.Children.Queries;
using MamConnect.Application.Dtos;
using MamConnect.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MamConnect.Api.Controllers;

[Authorize]
[ApiController]
[Route("children")]
public class ChildrenController : ControllerBase
{
    private readonly GetChildrenQuery _getChildrenQuery;
    private readonly GetChildrenWithRelationsQuery _getChildrenWithRelationsQuery;
    private readonly CreateChildCommand _createChildCommand;
    private readonly UpdateChildCommand _updateChildCommand;
    private readonly DeleteChildCommand _deleteChildCommand;
    private readonly ICurrentUserContext _currentUserContext;

    public ChildrenController(
        GetChildrenQuery getChildrenQuery,
        GetChildrenWithRelationsQuery getChildrenWithRelationsQuery,
        CreateChildCommand createChildCommand,
        UpdateChildCommand updateChildCommand,
        DeleteChildCommand deleteChildCommand,
        ICurrentUserContext currentUserContext)
    {
        _getChildrenQuery = getChildrenQuery;
        _getChildrenWithRelationsQuery = getChildrenWithRelationsQuery;
        _createChildCommand = createChildCommand;
        _updateChildCommand = updateChildCommand;
        _deleteChildCommand = deleteChildCommand;
        _currentUserContext = currentUserContext;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ChildResponseDto>>> Get()
    {
        CurrentUser? currentUser;
        bool userAvailable = _currentUserContext.TryGetCurrentUser(out currentUser);
        if (!userAvailable || currentUser == null)
        {
            return Unauthorized();
        }

        IReadOnlyCollection<Child> children = await _getChildrenQuery.ExecuteAsync(currentUser.UserId, currentUser.Role);
        List<ChildResponseDto> response = children
            .Select(child => child.ToResponseDto())
            .ToList();
        return Ok(response);
    }

    [HttpGet("with-relations")]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<IEnumerable<ChildRelationsDto>> GetWithRelations()
    {
        IReadOnlyCollection<ChildRelationsDto> children = await _getChildrenWithRelationsQuery.ExecuteAsync();
        return children;
    }

    [HttpPost]
    public async Task<ActionResult<ChildResponseDto>> Post(CreateChildRequestDto request, CancellationToken cancellationToken)
    {
        Child child = request.ToDomainEntity();
        CreateChildCommand.Result result = await _createChildCommand.ExecuteAsync(child, cancellationToken);
        if (result.Status == CreateChildCommand.ResultStatus.DuplicateFullName)
        {
            return Conflict(new { message = "Un enfant portant ce nom existe deja." });
        }

        Child? created = result.Child;
        if (created == null)
        {
            return StatusCode(500);
        }

        ChildResponseDto response = created.ToResponseDto();
        return Created($"/children/{created.Id}", response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, UpdateChildRequestDto request)
    {
        UpdateChildCommand.Input input = request.ToUpdateInput();
        bool updated = await _updateChildCommand.ExecuteAsync(id, input);
        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        bool deleted = await _deleteChildCommand.ExecuteAsync(id);
        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}
