using System;
using AutoMapper;
using DeviceService.Application.DTOs;
using DeviceService.Application.Features.DeviceType.Queries;
using DeviceService.Domain.Interfaces;
using MediatR;

namespace DeviceService.Application.Features.DeviceType.Handlers.QueryHandlers;

public class GetAllDeviceTypesQueryHandler(
    IDeviceTypeRepository deviceTypeRepository,
    IMapper mapper
) : IRequestHandler<GetAllDeviceTypesQuery, PaginatedResultDTO<DeviceTypeResponseDTO>>
{
    private readonly IDeviceTypeRepository _deviceTypeRepository = deviceTypeRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<PaginatedResultDTO<DeviceTypeResponseDTO>> Handle(
        GetAllDeviceTypesQuery request,
        CancellationToken cancellationToken
    )
    {
        var deviceTypes = await _deviceTypeRepository.GetAllAsync(request.Page, request.PageSize);
        var totalCount = await _deviceTypeRepository.CountAsync();
        var response = _mapper.Map<IEnumerable<DeviceTypeResponseDTO>>(deviceTypes);

        return new PaginatedResultDTO<DeviceTypeResponseDTO>(
            totalCount,
            request.Page,
            request.PageSize,
            response
        );
    }
}
