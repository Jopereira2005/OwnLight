using AutomationService.Application.Contracts.DTOs;
using MediatR;

namespace AutomationService.Application.Features.RoutineLog.Queries;

public class GetLogsByTargetIdQuery(Guid targetId, int pageNumber, int pageSize)
    : IRequest<PaginatedResultDTO<RoutineLogDTO>>
{
    public Guid TargetId { get; set; } = targetId;
    public int PageNumber { get; set; } = pageNumber;
    public int PageSize { get; set; } = pageSize;
}
