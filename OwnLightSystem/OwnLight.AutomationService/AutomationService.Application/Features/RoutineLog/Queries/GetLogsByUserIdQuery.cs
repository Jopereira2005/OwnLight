using AutomationService.Application.Contracts.DTOs;
using MediatR;

namespace AutomationService.Application.Features.RoutineLog.Queries;

public class GetLogsByUserIdQuery(int pageNumber, int pageSize)
    : IRequest<PaginatedResultDTO<RoutineLogDTO>>
{
    public int PageNumber { get; set; } = pageNumber;
    public int PageSize { get; set; } = pageSize;
}
