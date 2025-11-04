namespace MamConnect.Api.Validators;

using System;
using FluentValidation;
using MamConnect.Api.Dtos;

public sealed class CreateChildRequestValidator : AbstractValidator<CreateChildRequestDto>
{
    public CreateChildRequestValidator()
    {
        RuleFor(request => request.FirstName)
            .NotEmpty()
            .WithMessage("Le prenom est requis.")
            .MaximumLength(100)
            .WithMessage("Le prenom ne peut pas depasser 100 caracteres.");

        RuleFor(request => request.BirthDate)
            .NotNull()
            .WithMessage("La date de naissance est requise.")
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
            .WithMessage("La date de naissance doit etre anterieure ou egale a aujourd'hui.");

        RuleFor(request => request.AssistantId)
            .GreaterThan(0)
            .When(request => request.AssistantId.HasValue)
            .WithMessage("L'identifiant de l'assistante doit etre positif.");
    }
}

public sealed class UpdateChildRequestValidator : AbstractValidator<UpdateChildRequestDto>
{
    public UpdateChildRequestValidator()
    {
        RuleFor(request => request.FirstName)
            .NotEmpty()
            .WithMessage("Le prenom est requis.")
            .MaximumLength(100)
            .WithMessage("Le prenom ne peut pas depasser 100 caracteres.");

        RuleFor(request => request.BirthDate)
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
            .When(request => request.BirthDate.HasValue)
            .WithMessage("La date de naissance doit etre anterieure ou egale a aujourd'hui.");

        RuleFor(request => request.AssistantId)
            .GreaterThan(0)
            .When(request => request.AssistantId.HasValue)
            .WithMessage("L'identifiant de l'assistante doit etre positif.");
    }
}
