using System.Text.Json.Serialization;
using MediatR;

namespace AutomationService.Application.Features.Group.Commands;

public class DeleteGroupCommand : IRequest
{
    [JsonIgnore]
    public Guid Id { get; set; }
}
