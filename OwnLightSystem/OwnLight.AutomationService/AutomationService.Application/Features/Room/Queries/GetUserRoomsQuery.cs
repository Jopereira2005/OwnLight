using AutomationService.Application.Contracts.DTOs;
using MediatR;

namespace AutomationService.Application.Features.Room.Queries;

public class GetUserRoomsQuery(int pageNumber, int pageSize)
    : IRequest<PaginatedResultDTO<RoomResponseDTO>>
{
    public int PageNumber { get; set; } = pageNumber;
    public int PageSize { get; set; } = pageSize;
}
