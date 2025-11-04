namespace MamConnect.Api.Mappings;

using System.Collections.Generic;
using System.Linq;
using MamConnect.Api.Dtos;
using MamConnect.Domain.Entities;

public static class ParentMappingExtensions
{
    public static ParentResponseDto ToResponseDto(this User parent)
    {
        IEnumerable<int> childrenIds = parent.Children.Select(child => child.Id);
        ParentResponseDto dto = new ParentResponseDto(
            parent.Id,
            parent.Email,
            parent.FirstName,
            parent.LastName,
            parent.PhoneNumber,
            childrenIds);
        return dto;
    }
}
