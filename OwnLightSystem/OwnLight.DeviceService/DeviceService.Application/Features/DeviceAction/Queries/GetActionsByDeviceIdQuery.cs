using DeviceService.Application.DTOs;
using MediatR;

namespace DeviceService.Application.Features.DeviceAction.Queries;

public class GetActionsByDeviceIdQuery : IRequest<PaginatedResultDTO<ActionResponseDTO>>
{
    public Guid DeviceId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
