using AutoMapper;
using AutomationService.Application.Contracts.DTOs;
using AutomationService.Application.Features.RoutineLog.Queries;
using AutomationService.Domain.Interfaces;
using MediatR;

namespace AutomationService.Application.Features.RoutineLog.Handlers;

public class GetLogsByTargetIdQueryHandler(
    IRoutineExecutionLogRepository routineExecutionLogRepository,
    IMapper mapper
) : IRequestHandler<GetLogsByTargetIdQuery, PaginatedResultDTO<RoutineLogDTO>>
{
    private readonly IRoutineExecutionLogRepository _routineExecutionLogRepository =
        routineExecutionLogRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<PaginatedResultDTO<RoutineLogDTO>> Handle(
        GetLogsByTargetIdQuery request,
        CancellationToken cancellationToken
    )
    {
        var logs = await _routineExecutionLogRepository.GetByTaretId(
            request.TargetId,
            request.PageNumber,
            request.PageSize,
            cancellationToken
        );
        var totalCount = await _routineExecutionLogRepository.CountAsync(cancellationToken);
        var logsDTO = _mapper.Map<IEnumerable<RoutineLogDTO>>(logs);

        return new PaginatedResultDTO<RoutineLogDTO>(
            totalCount,
            request.PageNumber,
            request.PageSize,
            logsDTO
        );
    }
}
