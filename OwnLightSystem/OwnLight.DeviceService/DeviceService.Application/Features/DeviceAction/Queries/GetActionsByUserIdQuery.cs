using DeviceService.Application.DTOs;
using MediatR;

namespace DeviceService.Application.Features.DeviceAction.Queries;

public class GetActionsByUserIdQuery : IRequest<PaginatedResultDTO<ActionResponseDTO>>
{
    public Guid UserId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
