using AutoMapper;
using AutomationService.Application.Features.Room.Commands;
using AutomationService.Domain.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AutomationService.Application.Features.Room.Handlers.Commands;

public class UpdateRoomCommandHandler(
    IRoomRepository roomRepository,
    IMapper mapper,
    IValidator<UpdateRoomCommand> validator,
    IHttpContextAccessor httpContextAccessor
) : IRequestHandler<UpdateRoomCommand>
{
    private readonly IRoomRepository _roomRepository = roomRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<UpdateRoomCommand> _validator = validator;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Unit> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var userId = _httpContextAccessor.HttpContext?.Items["UserId"]?.ToString();
        if (string.IsNullOrEmpty(userId))
            throw new UnauthorizedAccessException("Falha ao obter o ID do usuário.");

        var room =
            await _roomRepository.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException("Cômodo não encontrado.");
        if (room.Name == request.Name)
            return Unit.Value;

        if (room.UserId.ToString() != userId)
            throw new UnauthorizedAccessException("Esse cômodo não pertence ao usuário logado.");

        var newName = await _roomRepository.GetUserRoomByNameAsync(
            room.UserId,
            request.Name,
            cancellationToken
        );

        if (newName == null)
        {
            _mapper.Map(request, room);
            await _roomRepository.UpdateAsync(room, cancellationToken);
            return Unit.Value;
        }
        else
            throw new InvalidOperationException("Usuário já possui um cômodo com esse nome.");
    }
}
