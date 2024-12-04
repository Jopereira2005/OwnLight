using System.Text.Json.Serialization;
using MediatR;

namespace DeviceService.Application.Features.DeviceAction.Commands;

public class DimmerizeGroupCommand : IRequest
{
    [JsonIgnore]
    public Guid UserId { get; set; }

    [JsonIgnore]
    public Guid GroupId { get; set; }
    public int Brightness { get; set; }
}
