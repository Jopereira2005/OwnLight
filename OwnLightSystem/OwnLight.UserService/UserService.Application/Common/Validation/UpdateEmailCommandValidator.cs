using FluentValidation;
using UserService.Application.Common.Services.Email;
using UserService.Application.Features.User.Commands.Update;

namespace UserService.Application.Common.Validation;

public class UpdateEmailCommandValidator : AbstractValidator<UpdateEmailCommand>
{
    public UpdateEmailCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .Must(x => !x.Contains(' '))
            .Must(EmailVerifier.IsValidDomain)
            .WithMessage("Endereço de email inválido");
    }
}
