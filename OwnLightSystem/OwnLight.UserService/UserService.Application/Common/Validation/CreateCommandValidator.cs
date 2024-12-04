using FluentValidation;
using UserService.Application.Common.Services.Email;
using UserService.Application.Features.User.Commands;

namespace UserService.Application.Common.Validation;

public class CreateCommandValidator : AbstractValidator<CreateCommand>
{
    public CreateCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .Length(3, 30)
            .WithMessage("Nome deve ter entre 3 e 30 caracteres");

        RuleFor(x => x.Username)
            .NotEmpty()
            .Length(3, 30)
            .Matches("^[a-zA-Z0-9]*$")
            .WithMessage("Nome de usuário inválido");

        RuleFor(x => x.Password)
            .NotEmpty()
            .Length(6, 20)
            .WithMessage("Senha deve ter entre 6 e 20 caracteres");

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .Must(x => !x.Contains(' '))
            .Must(EmailVerifier.IsValidDomain)
            .WithMessage("Endereço de email inválido");
    }
}
