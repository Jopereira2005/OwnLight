using AutoMapper;
using DeviceService.Application.DTOs;
using DeviceService.Application.Features.DeviceAction.Queries;
using DeviceService.Domain.Enums;
using DeviceService.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DeviceService.Application.Features.DeviceAction.Handlers.QueryHandlers;

public class GetUserActionsByTypeQueryHandler(
    IDeviceActionRepository deviceActionRepository,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor
) : IRequestHandler<GetUserActionsByTypeQuery, PaginatedResultDTO<ActionResponseDTO>>
{
    private readonly IDeviceActionRepository _deviceActionRepository = deviceActionRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<PaginatedResultDTO<ActionResponseDTO>> Handle(
        GetUserActionsByTypeQuery request,
        CancellationToken cancellationToken
    )
    {
        var userId = _httpContextAccessor.HttpContext?.Items["UserId"]?.ToString();
        if (string.IsNullOrEmpty(userId))
            throw new UnauthorizedAccessException("Usuário não autenticado.");

        var actions = await _deviceActionRepository.GetUserActionsByTypeAsync(
            Guid.Parse(userId),
            Enum.Parse<DeviceActions>(request.Action),
            request.PageNumber,
            request.PageSize
        );

        var totalCount = await _deviceActionRepository.CountAsync();
        var actionsDTO = _mapper.Map<IEnumerable<ActionResponseDTO>>(actions);

        return new PaginatedResultDTO<ActionResponseDTO>(
            totalCount,
            request.PageNumber,
            request.PageSize,
            actionsDTO
        );
    }
}
