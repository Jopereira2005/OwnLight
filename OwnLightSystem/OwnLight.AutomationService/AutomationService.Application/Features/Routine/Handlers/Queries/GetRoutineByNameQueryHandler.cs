using AutoMapper;
using AutomationService.Application.Contracts.DTOs;
using AutomationService.Application.Features.Routine.Queries;
using AutomationService.Domain.Interfaces;
using MediatR;

namespace AutomationService.Application.Features.Routine.Handlers.Queries;

public class GetRoutineByNameQueryHandler(IRoutineRepository routineRepository, IMapper mapper)
    : IRequestHandler<GetRoutineByNameQuery, RoutineResponseDTO>
{
    private readonly IRoutineRepository _routineRepository = routineRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<RoutineResponseDTO> Handle(
        GetRoutineByNameQuery request,
        CancellationToken cancellationToken
    )
    {
        var routine =
            await _routineRepository.GetRoutineByNameAsync(request.Name, cancellationToken)
            ?? throw new KeyNotFoundException($"Rotina com nome {request.Name} não encontrada.");

        return _mapper.Map<RoutineResponseDTO>(routine);
    }
}
