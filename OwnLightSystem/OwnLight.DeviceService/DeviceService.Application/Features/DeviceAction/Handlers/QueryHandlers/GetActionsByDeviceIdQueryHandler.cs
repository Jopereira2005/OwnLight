using AutoMapper;
using DeviceService.Application.DTOs;
using DeviceService.Application.Features.DeviceAction.Queries;
using DeviceService.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DeviceService.Application.Features.DeviceAction.Handlers.QueryHandlers;

public class GetActionsByDeviceIdQueryHandler(
    IDeviceActionRepository deviceActionRepository,
    IDeviceRepository deviceRepository,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor
) : IRequestHandler<GetActionsByDeviceIdQuery, PaginatedResultDTO<ActionResponseDTO>>
{
    private readonly IDeviceActionRepository _deviceActionRepository = deviceActionRepository;
    private readonly IDeviceRepository _deviceRepository = deviceRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<PaginatedResultDTO<ActionResponseDTO>> Handle(
        GetActionsByDeviceIdQuery request,
        CancellationToken cancellationToken
    )
    {
        var userId = _httpContextAccessor.HttpContext?.Items["UserId"]?.ToString();
        if (string.IsNullOrEmpty(userId))
            throw new UnauthorizedAccessException("Usuário não autenticado.");

        var device =
            await _deviceRepository.GetByIdAsync(request.DeviceId)
            ?? throw new KeyNotFoundException("Dispositivo não encontrado.");

        var actions = await _deviceActionRepository.GetActionsByDeviceIdAsync(
            device.Id,
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
