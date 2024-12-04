using System.ComponentModel;
using MediatR;

namespace AutomationService.Application.Features.Group.Commands;

public class CreateGroupCommand : IRequest<Guid>
{
    [DefaultValue("Group")]
    public required string Name { get; set; }

    [DefaultValue(null)]
    public string? Description { get; set; }
}
