using System.Text.Json;
using AutomationService.Application.Features.Group.Commands;
using AutomationService.Domain.Interfaces;
using MediatR;

namespace AutomationService.Application.Features.Group.Handlers.Commands;

public class AddGroupDevicesCommandHandler(IGroupRepository groupRepository)
    : IRequestHandler<AddGroupDevicesCommand, string>
{
    private readonly IGroupRepository _groupRepository = groupRepository;

    public async Task<string> Handle(
        AddGroupDevicesCommand request,
        CancellationToken cancellationToken
    )
    {
        var group =
            await _groupRepository.GetByIdAsync(request.GroupId)
            ?? throw new KeyNotFoundException("Grupo não encontrado.");

        if (request.DeviceIds == null || request.DeviceIds.Length == 0)
            throw new ArgumentException("É necessário informar ao menos um dispositivo.");

        var devices = await _groupRepository.GetGroupDevicesAsync(group.Id, cancellationToken);

        if (
            group.DeviceIds != null
            && request.DeviceIds.Any(id => group.DeviceIds.Contains(id.ToString()))
        )
            throw new InvalidOperationException("Um ou mais dispositivos já estão no grupo.");

        await _groupRepository.AddDevicesToGroupAsync(
            group.Id,
            request.DeviceIds,
            cancellationToken
        );

        var response = group.DeviceIds?.Split(',').Select(Guid.Parse).ToList();
        return JsonSerializer.Serialize(response);
    }
}
