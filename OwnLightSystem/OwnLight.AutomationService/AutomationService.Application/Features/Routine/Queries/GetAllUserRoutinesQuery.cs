using AutomationService.Application.Contracts.DTOs;
using MediatR;

namespace AutomationService.Application.Features.Routine.Queries;

public class GetAllUserRoutinesQuery(int pageNumber, int pageSize)
    : IRequest<PaginatedResultDTO<RoutineResponseDTO>>
{
    public int PageNumber { get; set; } = pageNumber;
    public int PageSize { get; set; } = pageSize;
}
