using AutoMapper;
using AutomationService.Application.Contracts.DTOs;
using AutomationService.Application.Features.Room.Queries;
using AutomationService.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AutomationService.Application.Features.Room.Handlers.Queries;

public class GetUserRoomsQueryHandler(
    IRoomRepository roomRepository,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor
) : IRequestHandler<GetUserRoomsQuery, PaginatedResultDTO<RoomResponseDTO>>
{
    private readonly IRoomRepository _roomRepository = roomRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<PaginatedResultDTO<RoomResponseDTO>> Handle(
        GetUserRoomsQuery request,
        CancellationToken cancellationToken
    )
    {
        var userId = _httpContextAccessor.HttpContext?.Items["UserId"]?.ToString();
        if (string.IsNullOrEmpty(userId))
            throw new UnauthorizedAccessException("Falha ao obter o ID do usuário.");

        var rooms = await _roomRepository.GetUserRoomsAsync(
            Guid.Parse(userId),
            request.PageNumber,
            request.PageSize,
            cancellationToken
        );

        var totalCount = await _roomRepository.CountAsync(cancellationToken);
        var roomsDTO = _mapper.Map<IEnumerable<RoomResponseDTO>>(rooms);

        return new PaginatedResultDTO<RoomResponseDTO>(
            totalCount,
            request.PageNumber,
            request.PageSize,
            roomsDTO
        );
    }
}
