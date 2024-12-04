using AutomationService.Application.Contracts.DTOs;
using MediatR;

namespace AutomationService.Application.Features.RoutineLog.Queries;

public class GetLogsByRoutineIdQuery(Guid routineId, int pageNumber, int pageSize)
    : IRequest<PaginatedResultDTO<RoutineLogDTO>>
{
    public Guid RoutineId { get; set; } = routineId;
    public int PageNumber { get; set; } = pageNumber;
    public int PageSize { get; set; } = pageSize;
}
