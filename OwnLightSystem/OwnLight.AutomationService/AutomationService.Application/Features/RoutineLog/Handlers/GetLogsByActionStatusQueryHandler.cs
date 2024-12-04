using AutoMapper;
using AutomationService.Application.Contracts.DTOs;
using AutomationService.Application.Features.RoutineLog.Queries;
using AutomationService.Domain.Enums;
using AutomationService.Domain.Interfaces;
using MediatR;

namespace AutomationService.Application.Features.RoutineLog.Handlers;

public class GetLogsByActionStatusQueryHandler(
    IRoutineExecutionLogRepository routineExecutionLogRepository,
    IMapper mapper
) : IRequestHandler<GetLogsByActionStatusQuery, PaginatedResultDTO<RoutineLogDTO>>
{
    private readonly IRoutineExecutionLogRepository _routineExecutionLogRepository =
        routineExecutionLogRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<PaginatedResultDTO<RoutineLogDTO>> Handle(
        GetLogsByActionStatusQuery request,
        CancellationToken cancellationToken
    )
    {
        var logs = await _routineExecutionLogRepository.GetByActionStatus(
            request.ActionStatus,
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
