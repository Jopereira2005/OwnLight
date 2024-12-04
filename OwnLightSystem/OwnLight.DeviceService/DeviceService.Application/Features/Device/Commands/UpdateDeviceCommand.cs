using System.Text.Json.Serialization;
using MediatR;

namespace DeviceService.Application.Features.Device.Commands;

public class UpdateDeviceCommand : IRequest
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public required string Name { get; set; }

    [JsonIgnore]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
