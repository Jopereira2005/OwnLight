using DeviceService.Application.DTOs;
using MediatR;

namespace DeviceService.Application.Features.Device.Queries;

public class GetAllDevicesQuery(int page, int pageSize) : IRequest<PaginatedResultDTO<DeviceResponseDTO>>
{
    public int Page { get; set; } = page;
    public int PageSize { get; set; } = pageSize;
}
