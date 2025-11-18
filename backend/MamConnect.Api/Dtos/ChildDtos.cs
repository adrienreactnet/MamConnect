namespace MamConnect.Api.Dtos;

using System;

public sealed class CreateChildRequestDto
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public DateOnly? BirthDate { get; set; }

    public int? AssistantId { get; set; }

    public string? Allergies { get; set; }

    public string? HeadshotUrl { get; set; }
}

public sealed class UpdateChildRequestDto
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public DateOnly? BirthDate { get; set; }

    public int? AssistantId { get; set; }

    public string? Allergies { get; set; }

    public string? HeadshotUrl { get; set; }
}

public sealed class ChildResponseDto
{
    public ChildResponseDto(
        int id,
        string firstName,
        string lastName,
        DateOnly? birthDate,
        int? assistantId,
        string? allergies,
        string? headshotUrl)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
        AssistantId = assistantId;
        Allergies = allergies;
        HeadshotUrl = headshotUrl;
    }

    public int Id { get; }

    public string FirstName { get; }

    public string LastName { get; }

    public DateOnly? BirthDate { get; }

    public int? AssistantId { get; }

    public string? Allergies { get; }

    public string? HeadshotUrl { get; }
}
