using System.ComponentModel;
using System.Text.Json.Serialization;
using MediatR;

namespace AutomationService.Application.Features.Group.Commands;

public class UpdateGroupCommand : IRequest
{
    [JsonIgnore]
    public Guid Id { get; set; }

    [DefaultValue("Group")]
    public required string Name { get; set; }

    [DefaultValue(null)]
    public string? Description { get; set; }

    [JsonIgnore]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
