using DeviceService.Application.DTOs;
using MediatR;

namespace DeviceService.Application.Features.DeviceType.Queries;

public class GetAllDeviceTypesQuery(int page, int pageSize) : IRequest<PaginatedResultDTO<DeviceTypeResponseDTO>>
{
    public int Page { get; set; } = page;
    public int PageSize { get; set; } = pageSize;
}
