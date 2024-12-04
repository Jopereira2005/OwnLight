using FluentValidation;
using UserService.Application.Features.User.Commands.Update;

namespace UserService.Application.Common.Validation;

public class UpdatePasswordCommandValidator : AbstractValidator<UpdatePasswordCommand>
{
    public UpdatePasswordCommandValidator()
    {
        RuleFor(x => x.NewPassword)
            .MinimumLength(6)
            .MaximumLength(30)
            .NotEmpty()
            .WithMessage("Senha deve ter entre 6 e 30 caracteres");
    }
}
