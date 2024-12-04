using AutoMapper;
using AutomationService.Application.Contracts.DTOs;
using AutomationService.Application.Features.Room.Queries;
using AutomationService.Domain.Interfaces;
using MediatR;

namespace AutomationService.Application.Features.Room.Handlers.Queries;

public class GetRoomDevicesQueryHandler(IRoomRepository roomRepository, IMapper mapper)
    : IRequestHandler<GetRoomDevicesQuery, RoomDevicesDTO>
{
    private readonly IRoomRepository _roomRepository = roomRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<RoomDevicesDTO> Handle(
        GetRoomDevicesQuery request,
        CancellationToken cancellationToken
    )
    {
        var room =
            await _roomRepository.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException("Sala não encontrada.");

        var roomDevices = await _roomRepository.GetRoomDevicesAsync(room.Id, cancellationToken);

        var result = _mapper.Map<RoomDevicesDTO>(roomDevices);
        return result;
    }
}
