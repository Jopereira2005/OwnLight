using AutoMapper;
using AutomationService.Application.Common.Services.Interfaces;
using AutomationService.Application.Features.Routine.Commands;
using AutomationService.Domain.Enums;
using AutomationService.Domain.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Entity = AutomationService.Domain.Entities;

namespace AutomationService.Application.Features.Routine.Handlers.Commands;

public class CreateRoutineCommandHandler(
    IRoutineRepository routineRepository,
    IRoutineSchedulerService schedulerService,
    IHttpContextAccessor httpContextAccessor,
    IMapper mapper,
    IValidator<CreateRoutineCommand> validator
) : IRequestHandler<CreateRoutineCommand, Guid>
{
    private readonly IRoutineRepository _routineRepository = routineRepository;
    private readonly IRoutineSchedulerService _schedulerService = schedulerService;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<CreateRoutineCommand> _validator = validator;

    public async Task<Guid> Handle(
        CreateRoutineCommand request,
        CancellationToken cancellationToken
    )
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);
        var userId = _httpContextAccessor.HttpContext?.Items["UserId"]?.ToString();
        if (string.IsNullOrEmpty(userId))
            throw new UnauthorizedAccessException("Falha ao obter o ID do usuário.");

        if (
            request.ActionTarget == ActionTarget.Home
            && request.ActionType == RoutineActionType.Dim
        )
            throw new InvalidOperationException("Dimmerização não é suportada para ações em casa.");

        if (request.IsOneTime == true && request.DaysOfWeek?.Any() == true)
            throw new InvalidOperationException(
                "Não é possível definir dias da semana para uma rotina única."
            );
        else if (request.IsCustom == true && request.DaysOfWeek?.Any() == false)
            throw new InvalidOperationException(
                "Dias da semana são obrigatórios para rotinas personalizadas."
            );
        else if (request.IsCustom == false)
            request.DaysOfWeek = null;

        var routine = _mapper.Map<Entity.Routine>(request);
        routine.UserId = Guid.Parse(userId);

        await _routineRepository.CreateAsync(routine, cancellationToken);
        await _schedulerService.ScheduleRoutineAsync(routine);

        return routine.Id;
    }
}
