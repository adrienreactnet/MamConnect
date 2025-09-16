namespace MamConnect.Api.Dtos;

using System.Collections.Generic;

public record ParentDto(
    int Id,
    string Email,
    string FirstName,
    string LastName,
    string PhoneNumber,
    IEnumerable<int> ChildrenIds
);