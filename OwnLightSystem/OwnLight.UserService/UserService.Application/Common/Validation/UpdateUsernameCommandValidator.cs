using FluentValidation;
using UserService.Application.Features.User.Commands.Update;

namespace UserService.Application.Common.Validation;

public class UpdateUsernameCommandValidator : AbstractValidator<UpdateUsernameCommand>
{
    public UpdateUsernameCommandValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(20)
            .Must(x => !x.Contains(' '))
            .WithMessage("Nome de usuário inválido");
    }
}
