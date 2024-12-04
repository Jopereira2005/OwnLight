using AutomationService.Application.Features.Routine.Commands;
using FluentValidation;

namespace AutomationService.Application.Common.Validations.Routine;

public class UpdateRoutineNameValidator : AbstractValidator<UpdateRoutineNameCommand>
{
    public UpdateRoutineNameValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Nome é obrigatório.")
            .MaximumLength(50)
            .WithMessage("Nome não pode ser maior que 50 caracteres.")
            .MinimumLength(3)
            .WithMessage("Nome não pode ser menor que 3 caracteres.");
    }
}
