using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MamConnect.Application.Common.Interfaces;
using MamConnect.Application.Dtos;
using MamConnect.Domain.Entities;

namespace MamConnect.Application.Children.Queries;

public class GetChildrenWithRelationsQuery
{
    private readonly IChildrenRepository _childrenRepository;

    public GetChildrenWithRelationsQuery(IChildrenRepository childrenRepository)
    {
        _childrenRepository = childrenRepository;
    }

    /// <summary>
    /// Retrieves all children with their assistant and parent relations.
    /// </summary>
    /// <returns>A read-only collection describing the relations of each child.</returns>
    public async Task<IReadOnlyCollection<ChildRelationsDto>> ExecuteAsync()
    {
        IReadOnlyCollection<Child> children = await _childrenRepository.GetChildrenWithRelationsAsync();
        List<ChildRelationsDto> result = children
            .Select(child => new ChildRelationsDto(
                child.FirstName,
                child.LastName,
                child.Assistant != null ? child.Assistant.FirstName + " " + child.Assistant.LastName : null,
                child.Parents.Select(parent => parent.FirstName + " " + parent.LastName).ToList()
            ))
            .ToList();
        return result;
    }
}
