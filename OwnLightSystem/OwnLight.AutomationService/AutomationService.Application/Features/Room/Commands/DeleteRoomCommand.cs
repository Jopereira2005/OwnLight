using MediatR;

namespace AutomationService.Application.Features.Room.Commands;

public class DeleteRoomCommand : IRequest
{
    public Guid Id { get; set; }
}
