using AutoMapper;
using AutomationService.Application.Contracts.DTOs;
using AutomationService.Application.Features.Routine.Queries;
using AutomationService.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AutomationService.Application.Features.Routine.Handlers.Queries;

public class GetAllUserRoutinesQueryHandler(
    IRoutineRepository routineRepository,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor
) : IRequestHandler<GetAllUserRoutinesQuery, PaginatedResultDTO<RoutineResponseDTO>>
{
    private readonly IRoutineRepository _routineRepository = routineRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<PaginatedResultDTO<RoutineResponseDTO>> Handle(
        GetAllUserRoutinesQuery request,
        CancellationToken cancellationToken
    )
    {
        var userId = _httpContextAccessor.HttpContext?.Items["UserId"]?.ToString();
        if (string.IsNullOrEmpty(userId))
            throw new UnauthorizedAccessException("Falha ao obter o ID do usuário.");

        var routines = await _routineRepository.GetUserRoutinesAsync(
            Guid.Parse(userId),
            request.PageNumber,
            request.PageSize,
            cancellationToken
        );
        var totalCount = await _routineRepository.CountAsync(cancellationToken);
        var routinesDTO = _mapper.Map<IEnumerable<RoutineResponseDTO>>(routines);

        return new PaginatedResultDTO<RoutineResponseDTO>(
            totalCount,
            request.PageNumber,
            request.PageSize,
            routinesDTO
        );
    }
}
