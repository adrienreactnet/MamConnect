using System;
using System.Collections.Generic;
using System.Threading;
using MamConnect.Application.Children.Commands;
using MamConnect.Application.Children.Queries;
using MamConnect.Application.Dtos;
using MamConnect.Api.Services;
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
    public async Task<ActionResult<IEnumerable<Child>>> Get()
    {
        CurrentUser? currentUser;
        bool userAvailable = _currentUserContext.TryGetCurrentUser(out currentUser);
        if (!userAvailable || currentUser == null)
        {
            return Unauthorized();
        }

        IReadOnlyCollection<Child> children = await _getChildrenQuery.ExecuteAsync(currentUser.UserId, currentUser.Role);
        return Ok(children);
    }

    [HttpGet("with-relations")]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<IEnumerable<ChildRelationsDto>> GetWithRelations()
    {
        IReadOnlyCollection<ChildRelationsDto> children = await _getChildrenWithRelationsQuery.ExecuteAsync();
        return children;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Child child, CancellationToken cancellationToken)
    {
        CreateChildCommand.Result result = await _createChildCommand.ExecuteAsync(child, cancellationToken);
        if (result.Status == CreateChildCommand.ResultStatus.DuplicateFirstName)
        {
            return Conflict(new { message = "Un enfant portant ce prénom existe déjà." });
        }

        Child? created = result.Child;
        if (created == null)
        {
            return StatusCode(500);
        }

        return Created($"/children/{created.Id}", created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Child input)
    {
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
