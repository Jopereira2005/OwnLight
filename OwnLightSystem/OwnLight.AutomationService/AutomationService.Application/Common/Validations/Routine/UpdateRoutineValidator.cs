using AutomationService.Application.Features.Routine.Commands;
using FluentValidation;

namespace AutomationService.Application.Common.Validations.Routine;

public class UpdateRoutineValidator : AbstractValidator<UpdateRoutineCommand>
{
    public UpdateRoutineValidator()
    {
        RuleFor(x => x.ActionType).IsInEnum().WithMessage("Tipo de ação inválido.");

        RuleFor(x => x.Brightness)
            .NotEmpty()
            .WithMessage("Brilho é obrigatório.")
            .InclusiveBetween(0, 100)
            .WithMessage("Brilho deve estar entre 0 e 100.");

        RuleFor(x => x.ExecutionTime)
            .Must(x => TimeSpan.TryParse(x.ToString(), out _))
            .WithMessage("Tempo de execução inválido.");
    }
}
