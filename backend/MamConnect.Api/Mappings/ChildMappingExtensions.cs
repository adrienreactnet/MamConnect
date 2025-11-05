namespace MamConnect.Api.Mappings;

using MamConnect.Api.Dtos;
using MamConnect.Application.Children.Commands;
using MamConnect.Domain.Entities;

public static class ChildMappingExtensions
{
    public static ChildResponseDto ToResponseDto(this Child child)
    {
        ChildResponseDto dto = new ChildResponseDto(
            child.Id,
            child.FirstName,
            child.LastName,
            child.BirthDate,
            child.AssistantId);
        return dto;
    }

    public static Child ToDomainEntity(this CreateChildRequestDto request)
    {
        Child child = new Child
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            BirthDate = request.BirthDate ?? default,
            AssistantId = request.AssistantId
        };
        return child;
    }

    public static UpdateChildCommand.Input ToUpdateInput(this UpdateChildRequestDto request)
    {
        UpdateChildCommand.Input input = new UpdateChildCommand.Input(
            request.FirstName,
            request.LastName,
            request.BirthDate,
            request.AssistantId);
        return input;
    }
}
