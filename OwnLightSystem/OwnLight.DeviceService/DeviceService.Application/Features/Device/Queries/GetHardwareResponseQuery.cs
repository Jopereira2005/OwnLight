using DeviceService.Application.DTOs;
using MediatR;

namespace DeviceService.Application.Features.Device.Queries;

public class GetHardwareResponseQuery(Guid[] deviceIds) : IRequest<IEnumerable<HardwareResponseDTO>>
{
    public Guid[] DeviceIds { get; } = deviceIds;
}
