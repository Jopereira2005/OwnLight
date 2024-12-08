using System.Text.Json;
using AutomationService.Application.Features.Room.Commands;
using AutomationService.Domain.Interfaces;
using MediatR;

namespace AutomationService.Application.Features.Room.Handlers.Commands;

public class AddRoomDevicesCommandHandler(IRoomRepository roomRepository)
    : IRequestHandler<AddRoomDevicesCommand, string>
{
    private readonly IRoomRepository _roomRepository = roomRepository;

    public async Task<string> Handle(
        AddRoomDevicesCommand request,
        CancellationToken cancellationToken
    )
    {
        var group =
            await _roomRepository.GetByIdAsync(request.GroupId)
            ?? throw new KeyNotFoundException("Grupo não encontrado.");

        if (request.DeviceIds == null || request.DeviceIds.Length == 0)
            throw new ArgumentException("É necessário informar ao menos um dispositivo.");

        var devices = await _roomRepository.GetRoomDevicesAsync(group.Id, cancellationToken);

        if (
            group.DeviceIds != null
            && request.DeviceIds.Any(id => group.DeviceIds.Contains(id.ToString()))
        )
            throw new InvalidOperationException("Um ou mais dispositivos já estão no grupo.");

        await _roomRepository.AddDevicesToRoomAsync(group.Id, request.DeviceIds, cancellationToken);

        var response = group.DeviceIds?.Split(',').Select(Guid.Parse).ToList();
        return JsonSerializer.Serialize(response);
    }
}
