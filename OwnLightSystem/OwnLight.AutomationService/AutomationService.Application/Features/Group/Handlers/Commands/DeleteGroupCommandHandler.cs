using AutomationService.Application.Features.Group.Commands;
using AutomationService.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AutomationService.Application.Features.Group.Handlers.Commands;

public class DeleteGroupCommandHandler(
    IGroupRepository groupRepository,
    IHttpContextAccessor httpContextAccessor
) : IRequestHandler<DeleteGroupCommand>
{
    private readonly IGroupRepository _groupRepository = groupRepository;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Unit> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext?.Items["UserId"]?.ToString();
        if (string.IsNullOrEmpty(userId))
            throw new UnauthorizedAccessException("Falha ao obter o ID do usuário.");

        var group =
            await _groupRepository.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException("Grupo não encontrado.");

        if (group.UserId.ToString() != userId)
            throw new UnauthorizedAccessException("Esse grupo não pertence ao usuário logado.");

        await _groupRepository.DeleteAsync(group.Id, cancellationToken);
        return Unit.Value;
    }
}
