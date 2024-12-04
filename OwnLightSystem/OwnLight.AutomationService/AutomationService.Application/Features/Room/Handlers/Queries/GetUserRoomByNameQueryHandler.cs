using AutoMapper;
using AutomationService.Application.Contracts.DTOs;
using AutomationService.Application.Features.Room.Queries;
using AutomationService.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AutomationService.Application.Features.Room.Handlers.Queries;

public class GetUserRoomByNameQueryHandler(
    IRoomRepository roomRepository,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor
) : IRequestHandler<GetUserRoomByNameQuery, RoomResponseDTO>
{
    private readonly IRoomRepository _roomRepository = roomRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<RoomResponseDTO> Handle(
        GetUserRoomByNameQuery request,
        CancellationToken cancellationToken
    )
    {
        var userId = _httpContextAccessor.HttpContext?.Items["UserId"]?.ToString();
        if (string.IsNullOrEmpty(userId))
            throw new UnauthorizedAccessException("Falha ao obter o ID do usuário.");

        var room =
            await _roomRepository.GetUserRoomByNameAsync(
                Guid.Parse(userId),
                request.Name,
                cancellationToken
            ) ?? throw new KeyNotFoundException("Cômodo não encontrado.");

        return _mapper.Map<RoomResponseDTO>(room);
    }
}
