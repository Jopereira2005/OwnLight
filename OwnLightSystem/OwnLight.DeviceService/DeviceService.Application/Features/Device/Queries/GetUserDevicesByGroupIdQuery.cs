using DeviceService.Application.DTOs;
using MediatR;

namespace DeviceService.Application.Features.Device.Queries;

public class GetUserDevicesByGroupIdQuery(Guid groupId, int pageNumber, int pageSize)
    : IRequest<PaginatedResultDTO<UserResponseDTO>>
{
    public Guid GroupId { get; set; } = groupId;
    public int PageNumber { get; set; } = pageNumber;
    public int PageSize { get; set; } = pageSize;
}
