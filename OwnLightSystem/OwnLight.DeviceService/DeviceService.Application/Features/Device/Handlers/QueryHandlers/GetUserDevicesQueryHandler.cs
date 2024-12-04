using AutoMapper;
using DeviceService.Application.DTOs;
using DeviceService.Application.Features.Device.Queries;
using DeviceService.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DeviceService.Application.Features.Device.Handlers.QueryHandlers;

public class GetUserDevicesQueryHandler
    : IRequestHandler<GetUserDevicesQuery, PaginatedResultDTO<UserResponseDTO>>
{
    private readonly IDeviceRepository _deviceRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetUserDevicesQueryHandler(
        IDeviceRepository deviceRepository,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _deviceRepository = deviceRepository;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<PaginatedResultDTO<UserResponseDTO>> Handle(
        GetUserDevicesQuery request,
        CancellationToken cancellationToken
    )
    {
        var userId = _httpContextAccessor.HttpContext?.Items["UserId"]?.ToString();
        if (string.IsNullOrEmpty(userId))
            throw new UnauthorizedAccessException("Usuário não autenticado.");

        var devices = await _deviceRepository.GetDevicesByUserIdAsync(
            Guid.Parse(userId),
            request.PageNumber,
            request.PageSize
        );
        var totalCount = await _deviceRepository.CountAsync();
        var devicesDTO = _mapper.Map<IEnumerable<UserResponseDTO>>(devices);

        return new PaginatedResultDTO<UserResponseDTO>(
            totalCount,
            request.PageNumber,
            request.PageSize,
            devicesDTO
        );
    }
}
