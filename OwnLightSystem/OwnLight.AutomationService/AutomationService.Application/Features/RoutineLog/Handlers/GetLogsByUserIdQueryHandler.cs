using AutoMapper;
using AutomationService.Application.Contracts.DTOs;
using AutomationService.Application.Features.RoutineLog.Queries;
using AutomationService.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AutomationService.Application.Features.RoutineLog.Handlers;

public class GetLogsByUserIdQueryHandler(
    IRoutineExecutionLogRepository routineRepository,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor
) : IRequestHandler<GetLogsByUserIdQuery, PaginatedResultDTO<RoutineLogDTO>>
{
    private readonly IRoutineExecutionLogRepository _logRepository = routineRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<PaginatedResultDTO<RoutineLogDTO>> Handle(
        GetLogsByUserIdQuery request,
        CancellationToken cancellationToken
    )
    {
        var userId = _httpContextAccessor.HttpContext?.Items["UserId"]?.ToString();
        if (string.IsNullOrEmpty(userId))
            throw new UnauthorizedAccessException("Falha ao obter o ID do usuário.");

        var logs = await _logRepository.GetByUserId(
            Guid.Parse(userId),
            request.PageNumber,
            request.PageSize,
            cancellationToken
        );
        var totalCount = await _logRepository.CountAsync(cancellationToken);
        var logsDTO = _mapper.Map<IEnumerable<RoutineLogDTO>>(logs);

        return new PaginatedResultDTO<RoutineLogDTO>(
            totalCount,
            request.PageNumber,
            request.PageSize,
            logsDTO
        );
    }
}
