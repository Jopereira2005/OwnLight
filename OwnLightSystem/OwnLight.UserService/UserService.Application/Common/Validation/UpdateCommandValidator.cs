using FluentValidation;
using UserService.Application.Common.Services.Email;
using UserService.Application.Features.User.Commands.Update;

namespace UserService.Application.Common.Validation;

public class UpdateCommandValidator : AbstractValidator<UpdateCommand>
{
    public UpdateCommandValidator()
    {
        RuleFor(x => x.Name)
            .MinimumLength(3)
            .MaximumLength(30)
            .NotEmpty()
            .WithMessage("Nome deve ter entre 3 e 30 caracteres");
    }
}
