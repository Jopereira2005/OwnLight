using AutomationService.Application.Features.Room.Commands;
using AutomationService.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AutomationService.Application.Features.Room.Handlers.Commands;

public class DeleteRoomCommandHandler(
    IRoomRepository roomRepository,
    IHttpContextAccessor httpContextAccessor
) : IRequestHandler<DeleteRoomCommand>
{
    private readonly IRoomRepository _roomRepository = roomRepository;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

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

        await _roomRepository.DeleteAsync(room.Id, cancellationToken);
        return Unit.Value;
    }
}
