using AutoMapper;
using AutomationService.Application.Contracts.DTOs;
using AutomationService.Application.Features.Group.Queries;
using AutomationService.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AutomationService.Application.Features.Group.Handlers.Queries;

public class GetUserGroupsQueryHandler(
    IGroupRepository groupRepository,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor
) : IRequestHandler<GetUserGroupsQuery, PaginatedResultDTO<GroupResponseDTO>>
{
    private readonly IGroupRepository _groupRepository = groupRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<PaginatedResultDTO<GroupResponseDTO>> Handle(
        GetUserGroupsQuery request,
        CancellationToken cancellationToken
    )
    {
        var userId = _httpContextAccessor.HttpContext?.Items["UserId"]?.ToString();
        if (string.IsNullOrEmpty(userId))
            throw new UnauthorizedAccessException("Falha ao obter o ID do usuário.");

        var groups = await _groupRepository.GetUserGroupsAsync(
            Guid.Parse(userId),
            request.PageNumber,
            request.PageSize,
            cancellationToken
        );

        var totalCount = await _groupRepository.CountAsync(cancellationToken);
        var groupsDTO = _mapper.Map<IEnumerable<GroupResponseDTO>>(groups);

        return new PaginatedResultDTO<GroupResponseDTO>(
            totalCount,
            request.PageNumber,
            request.PageSize,
            groupsDTO
        );
    }
}
