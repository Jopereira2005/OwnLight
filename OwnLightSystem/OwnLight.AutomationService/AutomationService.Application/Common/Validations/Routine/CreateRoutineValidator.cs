using System.Data;
using AutomationService.Application.Features.Routine.Commands;
using FluentValidation;

namespace AutomationService.Application.Common.Validations.Routine;

public class CreateRoutineValidator : AbstractValidator<CreateRoutineCommand>
{
    public CreateRoutineValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Nome é obrigatório.")
            .MaximumLength(50)
            .WithMessage("Nome não pode ser maior que 50 caracteres.")
            .MinimumLength(3)
            .WithMessage("Nome não pode ser menor que 3 caracteres.");

        RuleFor(x => x.ActionTarget).IsInEnum().WithMessage("Alvo inválido.");

        RuleFor(x => x.ActionType).IsInEnum().WithMessage("Tipo de ação inválido.");

        RuleFor(x => x.ExecutionTime)
            .NotEmpty()
            .WithMessage("Tempo de execução é obrigatório.")
            .Must(x => TimeSpan.TryParse(x.ToString(), out _))
            .WithMessage("Tempo de execução inválido.");

        RuleFor(x => x.TargetId)
            .NotEmpty()
            .WithMessage("Id do alvo é obrigatório.")
            .Must(x => Guid.TryParse(x.ToString(), out _))
            .WithMessage("Id do alvo inválido.");

        RuleFor(x => x.Brightness)
            .NotEmpty()
            .WithMessage("Brilho é obrigatório.")
            .InclusiveBetween(0, 100)
            .WithMessage("Brilho deve estar entre 0 e 100.");
    }
}
