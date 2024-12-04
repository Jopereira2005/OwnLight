using System.Text.Json.Serialization;
using MediatR;

namespace DeviceService.Application.Features.DeviceAction.Commands;

public class DimmerizeDeviceCommand(Guid deviceId, int brightness) : IRequest
{
    [JsonIgnore]
    public Guid DeviceId { get; set; } = deviceId;
    public int Brightness { get; set; } = brightness;
}
