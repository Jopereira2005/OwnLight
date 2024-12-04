using DeviceService.Application.DTOs;
using MediatR;

namespace DeviceService.Application.Features.DeviceType.Queries;

public class GetDeviceTypeByIdQuery(Guid id) : IRequest<DeviceTypeResponseDTO>
{
    public Guid Id { get; set; } = id;
}
