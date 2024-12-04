using AutoMapper;
using AutomationService.Application.Features.Room.Commands;
using AutomationService.Domain.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Entity = AutomationService.Domain.Entities;

namespace AutomationService.Application.Features.Room.Handlers.Commands;

public class CreateRoomCommandHandler(
    IRoomRepository roomRepository,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor,
    IValidator<CreateRoomCommand> validator
) : IRequestHandler<CreateRoomCommand, Guid>
{
    private readonly IRoomRepository _roomRepository = roomRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IValidator<CreateRoomCommand> _validator = validator;

    public async Task<Guid> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var userId = _httpContextAccessor.HttpContext?.Items["UserId"]?.ToString();
        if (string.IsNullOrEmpty(userId))
            throw new UnauthorizedAccessException("Falha ao obter o ID do usuário.");

        var room = _mapper.Map<Entity.Room>(request);
        room.UserId = Guid.Parse(userId);

        var roomName = await _roomRepository.GetUserRoomByNameAsync(
            room.UserId,
            room.Name,
            cancellationToken
        );

        if (roomName == null)
        {
            await _roomRepository.CreateAsync(room, cancellationToken);
            return room.Id;
        }
        else
            throw new InvalidOperationException("Usuário já possui um cômodo com esse nome.");
    }
}
