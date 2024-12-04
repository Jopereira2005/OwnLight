using System.Text.Json.Serialization;
using MediatR;

namespace DeviceService.Application.Features.DeviceType.Commands;

public class CreateDeviceTypeCommand : IRequest
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public string TypeName { get; set; } = null!;
}
