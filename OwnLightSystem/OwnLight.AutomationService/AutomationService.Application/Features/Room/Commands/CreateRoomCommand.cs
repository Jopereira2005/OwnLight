using System.ComponentModel;
using MediatR;

namespace AutomationService.Application.Features.Room.Commands;

public class CreateRoomCommand : IRequest<Guid>
{
    [DefaultValue("Room")]
    public required string Name { get; set; }
}
