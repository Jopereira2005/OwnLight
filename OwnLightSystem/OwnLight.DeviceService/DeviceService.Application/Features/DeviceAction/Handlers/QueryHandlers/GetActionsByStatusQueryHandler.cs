using AutoMapper;
using DeviceService.Application.DTOs;
using DeviceService.Application.Features.DeviceAction.Queries;
using DeviceService.Domain.Enums;
using DeviceService.Domain.Interfaces;
using MediatR;

namespace DeviceService.Application.Features.DeviceAction.Handlers.QueryHandlers;

public class GetActionsByStatusQueryHandler(
    IDeviceActionRepository deviceActionRepository,
    IMapper mapper
) : IRequestHandler<GetActionsByStatusQuery, PaginatedResultDTO<ActionResponseDTO>>
{
    private readonly IDeviceActionRepository _deviceActionRepository = deviceActionRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<PaginatedResultDTO<ActionResponseDTO>> Handle(
        GetActionsByStatusQuery request,
        CancellationToken cancellationToken
    )
    {
        var actions =
            await _deviceActionRepository.GetActionsByStatusAsync(
                Enum.Parse<ActionStatus>(request.Status),
                request.PageNumber,
                request.PageSize
            ) ?? throw new KeyNotFoundException("No actions found.");

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
