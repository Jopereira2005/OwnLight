using DeviceService.Application.DTOs;
using MediatR;

namespace DeviceService.Application.Features.Device.Queries;

public class GetUserDevicesQuery(int pageNumber, int pageSize)
    : IRequest<PaginatedResultDTO<UserResponseDTO>>
{
    public int PageNumber { get; set; } = pageNumber;
    public int PageSize { get; set; } = pageSize;
}
