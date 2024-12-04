using AutoMapper;
using AutomationService.Application.Contracts.DTOs;
using AutomationService.Application.Features.Group.Queries;
using AutomationService.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AutomationService.Application.Features.Group.Handlers.Queries;

public class GetUserGroupByNameQueryHandler(
    IGroupRepository groupRepository,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor
) : IRequestHandler<GetUserGroupByNameQuery, GroupResponseDTO>
{
    private readonly IGroupRepository _groupRepository = groupRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<GroupResponseDTO> Handle(
        GetUserGroupByNameQuery request,
        CancellationToken cancellationToken
    )
    {
        var userId = _httpContextAccessor.HttpContext?.Items["UserId"]?.ToString();
        if (string.IsNullOrEmpty(userId))
            throw new UnauthorizedAccessException("Falha ao obter o ID do usuário.");

        var group =
            await _groupRepository.GetUserGroupByNameAsync(
                Guid.Parse(userId),
                request.Name,
                cancellationToken
            ) ?? throw new KeyNotFoundException("Grupo não encontrado.");

        return _mapper.Map<GroupResponseDTO>(group);
    }
}
