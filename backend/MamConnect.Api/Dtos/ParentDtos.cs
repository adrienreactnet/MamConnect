namespace MamConnect.Api.Dtos;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

public sealed class CreateParentRequestDto
{
    public string Email { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    [SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Required for model binding.")]
    public List<int> ChildrenIds { get; set; } = new List<int>();
}

public sealed class UpdateParentRequestDto
{
    public string Email { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;
}

public sealed class ParentResponseDto
{
    public ParentResponseDto(int id, string email, string firstName, string lastName, string phoneNumber, IEnumerable<int> childrenIds)
    {
        Id = id;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        ChildrenIds = childrenIds;
    }

    public int Id { get; }

    public string Email { get; }

    public string FirstName { get; }

    public string LastName { get; }

    public string PhoneNumber { get; }

    public IEnumerable<int> ChildrenIds { get; }
}
