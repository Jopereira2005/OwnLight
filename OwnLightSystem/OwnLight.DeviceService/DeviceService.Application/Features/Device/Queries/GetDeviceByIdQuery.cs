using DeviceService.Application.DTOs;
using MediatR;

namespace DeviceService.Application.Features.Device.Queries;

public class GetDeviceByIdQuery(Guid id) : IRequest<DeviceResponseDTO>
{
    public Guid Id { get; } = id;
}
