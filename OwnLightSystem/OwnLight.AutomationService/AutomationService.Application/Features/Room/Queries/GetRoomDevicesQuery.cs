using AutomationService.Application.Contracts.DTOs;
using MediatR;

namespace AutomationService.Application.Features.Room.Queries;

public class GetRoomDevicesQuery(Guid id) : IRequest<RoomDevicesDTO>
{
    public Guid Id { get; set; } = id;
}
