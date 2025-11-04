namespace MamConnect.Api.Dtos;

using System;

public sealed class CreateChildRequestDto
{
    public string FirstName { get; set; } = string.Empty;

    public DateOnly? BirthDate { get; set; }

    public int? AssistantId { get; set; }
}

public sealed class UpdateChildRequestDto
{
    public string FirstName { get; set; } = string.Empty;

    public DateOnly? BirthDate { get; set; }

    public int? AssistantId { get; set; }
}

public sealed class ChildResponseDto
{
    public ChildResponseDto(int id, string firstName, DateOnly? birthDate, int? assistantId)
    {
        Id = id;
        FirstName = firstName;
        BirthDate = birthDate;
        AssistantId = assistantId;
    }

    public int Id { get; }

    public string FirstName { get; }

    public DateOnly? BirthDate { get; }

    public int? AssistantId { get; }
}
