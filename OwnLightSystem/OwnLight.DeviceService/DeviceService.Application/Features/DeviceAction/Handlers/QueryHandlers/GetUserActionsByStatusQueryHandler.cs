using AutoMapper;
using DeviceService.Application.DTOs;
using DeviceService.Application.Features.DeviceAction.Queries;
using DeviceService.Domain.Enums;
using DeviceService.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DeviceService.Application.Features.DeviceAction.Handlers.QueryHandlers;

public class GetUserActionsByStatusQueryHandler(
    IDeviceActionRepository deviceActionRepository,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor
) : IRequestHandler<GetUserActionsByStatusQuery, PaginatedResultDTO<ActionResponseDTO>>
{
    private readonly IDeviceActionRepository _deviceActionRepository = deviceActionRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<PaginatedResultDTO<ActionResponseDTO>> Handle(
        GetUserActionsByStatusQuery request,
        CancellationToken cancellationToken
    )
    {
        var userId = _httpContextAccessor.HttpContext?.Items["UserId"]?.ToString();
        if (string.IsNullOrEmpty(userId))
            throw new UnauthorizedAccessException("Usuário não autenticado.");

        var status = Enum.Parse<ActionStatus>(request.Status);
        if (!Enum.IsDefined(typeof(ActionStatus), status))
        {
            var message = $"Status {request.Status} não é válido.";
            throw new KeyNotFoundException(message);
        }

        var actions = await _deviceActionRepository.GetUserActionsByStatusAsync(
            Guid.Parse(userId),
            status,
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
