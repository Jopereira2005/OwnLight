using AutomationService.Application.Contracts.DTOs;
using MediatR;

namespace AutomationService.Application.Features.Room.Queries;

public class GetUserRoomByNameQuery(string name) : IRequest<RoomResponseDTO>
{
    public string Name { get; set; } = name;
}
