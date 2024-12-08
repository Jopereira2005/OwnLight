using AutomationService.Application.Common.Services.Interfaces;
using AutomationService.Application.Features.Room.Commands;
using AutomationService.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AutomationService.Application.Features.Room.Handlers.Commands;

public class DeleteRoomCommandHandler(
    IRoomRepository roomRepository,
    IHttpContextAccessor httpContextAccessor,
    IDeviceServiceClient deviceServiceClient
) : IRequestHandler<DeleteRoomCommand>
{
    private readonly IRoomRepository _roomRepository = roomRepository;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IDeviceServiceClient _deviceServiceClient = deviceServiceClient;

    public async Task<Unit> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext?.Items["UserId"]?.ToString();
        if (string.IsNullOrEmpty(userId))
            throw new UnauthorizedAccessException("Falha ao obter o ID do usuário.");

        var room =
            await _roomRepository.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException("Cômodo não encontrado.");

        if (room.UserId.ToString() != userId)
            throw new UnauthorizedAccessException("Esse cômodo não pertence ao usuário logado.");

        var accessToken = _httpContextAccessor
            .HttpContext?.Request.Headers["Authorization"]
            .ToString()
            .Replace("Bearer ", "");

        if (string.IsNullOrEmpty(accessToken))
            throw new UnauthorizedAccessException("JWT token ausente.");

        var response = await _deviceServiceClient.DeleteDevicesByRoomIdAsync(room.Id, accessToken);
        if (!response.IsSuccess)
            throw new Exception(
                $"Falha ao deletar dispositivos do cômodo na API DeviceService: {response.ErrorMessage}"
            );

        await _roomRepository.DeleteAsync(room.Id, cancellationToken);

        return Unit.Value;
    }
}
