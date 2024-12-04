using System.Text.Json;
using AutomationService.Application.Features.Room.Commands;
using AutomationService.Domain.Interfaces;
using MediatR;

namespace AutomationService.Application.Features.Room.Handlers.Commands;

public class RemoveRoomDevicesCommandHandler(IRoomRepository roomRepository)
    : IRequestHandler<RemoveRoomDevicesCommand, string>
{
    private readonly IRoomRepository _roomRepository = roomRepository;

    public async Task<string> Handle(
        RemoveRoomDevicesCommand request,
        CancellationToken cancellationToken
    )
    {
        var group =
            await _roomRepository.GetByIdAsync(request.GroupId)
            ?? throw new KeyNotFoundException("Grupo não encontrado.");

        if (request.DeviceIds == null || request.DeviceIds.Length == 0)
            throw new ArgumentException("É necessário informar ao menos um dispositivo.");

        await _roomRepository.RemoveDevicesFromRoomAsync(
            group.Id,
            request.DeviceIds,
            cancellationToken
        );
        if (string.IsNullOrEmpty(group.DeviceIds))
            return JsonSerializer.Serialize(Array.Empty<Guid>());

        var response = group
            .DeviceIds?.Split(',')
            .Where(id => Guid.TryParse(id, out _))
            .Select(Guid.Parse)
            .ToArray();
        return JsonSerializer.Serialize(response);
    }
}
