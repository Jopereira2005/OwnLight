using AutomationService.Application.Contracts.DTOs;
using MediatR;

namespace AutomationService.Application.Features.Group.Queries;

public class GetUserGroupsQuery(int pageNumber, int pageSize)
    : IRequest<PaginatedResultDTO<GroupResponseDTO>>
{
    public int PageNumber { get; set; } = pageNumber;
    public int PageSize { get; set; } = pageSize;
}
