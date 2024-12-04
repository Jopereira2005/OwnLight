using System.Text.Json.Serialization;
using MediatR;

namespace AutomationService.Application.Features.Routine.Commands;

public class UpdateRoutineNameCommand : IRequest
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public required string Name { get; set; }
}
