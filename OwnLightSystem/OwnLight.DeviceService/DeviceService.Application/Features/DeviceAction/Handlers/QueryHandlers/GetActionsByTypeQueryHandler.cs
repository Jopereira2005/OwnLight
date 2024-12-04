using AutoMapper;
using DeviceService.Application.DTOs;
using DeviceService.Application.Features.DeviceAction.Queries;
using DeviceService.Domain.Enums;
using DeviceService.Domain.Interfaces;
using MediatR;

namespace DeviceService.Application.Features.DeviceAction.Handlers.QueryHandlers;

public class GetActionsByTypeQueryHandler(
    IDeviceActionRepository deviceActionRepository,
    IMapper mapper
) : IRequestHandler<GetActionsByTypeQuery, PaginatedResultDTO<ActionResponseDTO>>
{
    private readonly IDeviceActionRepository _deviceActionRepository = deviceActionRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<PaginatedResultDTO<ActionResponseDTO>> Handle(
        GetActionsByTypeQuery request,
        CancellationToken cancellationToken
    )
    {
        var actions =
            await _deviceActionRepository.GetActionsByTypeAsync(
                Enum.Parse<DeviceActions>(request.Action),
                request.PageNumber,
                request.PageSize
            ) ?? throw new KeyNotFoundException("Nenhuma ação encontrada.");
            
        var totalCount = await _deviceActionRepository.CountAsync();
        var mappedActions = _mapper.Map<IEnumerable<ActionResponseDTO>>(actions);

        return new PaginatedResultDTO<ActionResponseDTO>(
            totalCount,
            request.PageNumber,
            request.PageSize,
            mappedActions
        );
    }
}
