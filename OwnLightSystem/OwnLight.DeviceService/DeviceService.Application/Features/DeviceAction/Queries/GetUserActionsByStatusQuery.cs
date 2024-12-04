using DeviceService.Application.DTOs;
using MediatR;

namespace DeviceService.Application.Features.DeviceAction.Queries;

public class GetUserActionsByStatusQuery : IRequest<PaginatedResultDTO<ActionResponseDTO>>
{
    public Guid UserId { get; set; }
    public required string Status { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
