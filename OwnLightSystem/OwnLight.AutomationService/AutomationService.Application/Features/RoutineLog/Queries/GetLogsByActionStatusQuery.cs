using AutomationService.Application.Contracts.DTOs;
using AutomationService.Domain.Enums;
using MediatR;

namespace AutomationService.Application.Features.RoutineLog.Queries;

public class GetLogsByActionStatusQuery(ActionStatus actionStatus, int pageNumber, int pageSize)
    : IRequest<PaginatedResultDTO<RoutineLogDTO>>
{
    public ActionStatus ActionStatus { get; set; } = actionStatus;
    public int PageNumber { get; set; } = pageNumber;
    public int PageSize { get; set; } = pageSize;
}
