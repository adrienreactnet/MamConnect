using System.Collections.Generic;

namespace MamConnect.Application.Dtos;

public record ChildRelationsDto(
    string ChildFirstName,
    string? AssistantName,
    IReadOnlyCollection<string> ParentNames
);
