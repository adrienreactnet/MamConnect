namespace MamConnect.Api.Validators;

using FluentValidation;
using MamConnect.Api.Dtos;

public sealed class CreateParentRequestValidator : AbstractValidator<CreateParentRequestDto>
{
    public CreateParentRequestValidator()
    {
        RuleFor(request => request.Email)
            .NotEmpty()
            .WithMessage("L'adresse e-mail est requise.")
            .EmailAddress()
            .WithMessage("L'adresse e-mail n'est pas valide.");

        RuleFor(request => request.FirstName)
            .NotEmpty()
            .WithMessage("Le prenom est requis.")
            .MaximumLength(100)
            .WithMessage("Le prenom ne peut pas depasser 100 caracteres.");

        RuleFor(request => request.LastName)
            .NotEmpty()
            .WithMessage("Le nom est requis.")
            .MaximumLength(100)
            .WithMessage("Le nom ne peut pas depasser 100 caracteres.");

        RuleFor(request => request.PhoneNumber)
            .MaximumLength(20)
            .WithMessage("Le numero de telephone ne peut pas depasser 20 caracteres.");

        RuleForEach(request => request.ChildrenIds)
            .GreaterThan(0)
            .WithMessage("L'identifiant d'enfant doit etre positif.");
    }
}

public sealed class UpdateParentRequestValidator : AbstractValidator<UpdateParentRequestDto>
{
    public UpdateParentRequestValidator()
    {
        RuleFor(request => request.Email)
            .NotEmpty()
            .WithMessage("L'adresse e-mail est requise.")
            .EmailAddress()
            .WithMessage("L'adresse e-mail n'est pas valide.");

        RuleFor(request => request.FirstName)
            .NotEmpty()
            .WithMessage("Le prenom est requis.")
            .MaximumLength(100)
            .WithMessage("Le prenom ne peut pas depasser 100 caracteres.");

        RuleFor(request => request.LastName)
            .NotEmpty()
            .WithMessage("Le nom est requis.")
            .MaximumLength(100)
            .WithMessage("Le nom ne peut pas depasser 100 caracteres.");

        RuleFor(request => request.PhoneNumber)
            .MaximumLength(20)
            .WithMessage("Le numero de telephone ne peut pas depasser 20 caracteres.");
    }
}
