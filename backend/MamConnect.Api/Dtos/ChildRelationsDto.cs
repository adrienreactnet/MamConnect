using System.Collections.Generic;

namespace MamConnect.Api.Dtos;

public record ChildRelationsDto(
    string ChildFirstName,
    string? AssistantName,
    IReadOnlyCollection<string> ParentNames
);