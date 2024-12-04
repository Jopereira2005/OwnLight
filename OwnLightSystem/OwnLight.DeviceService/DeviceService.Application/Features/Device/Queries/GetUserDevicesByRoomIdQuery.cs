using DeviceService.Application.DTOs;
using MediatR;

namespace DeviceService.Application.Features.Device.Queries;

public class GetUserDevicesByRoomIdQuery(Guid roomId, int pageNumber, int pageSize)
    : IRequest<PaginatedResultDTO<UserResponseDTO>>
{
    public Guid RoomId { get; set; } = roomId;
    public int PageNumber { get; set; } = pageNumber;
    public int PageSize { get; set; } = pageSize;
}
