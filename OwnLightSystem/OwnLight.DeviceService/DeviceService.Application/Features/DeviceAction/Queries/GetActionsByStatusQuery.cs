using DeviceService.Application.DTOs;
using MediatR;

namespace DeviceService.Application.Features.DeviceAction.Queries;

public class GetActionsByStatusQuery : IRequest<PaginatedResultDTO<ActionResponseDTO>>
{
    public required string Status { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
