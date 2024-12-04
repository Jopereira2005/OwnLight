using AutoMapper;
using AutomationService.Application.Features.Group.Commands;
using AutomationService.Domain.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AutomationService.Application.Features.Group.Handlers.Commands;

public class UpdateGroupCommandHandler(
    IGroupRepository groupRepository,
    IMapper mapper,
    IValidator<UpdateGroupCommand> validator,
    IHttpContextAccessor httpContextAccessor
) : IRequestHandler<UpdateGroupCommand>
{
    private readonly IGroupRepository _groupRepository = groupRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<UpdateGroupCommand> _validator = validator;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Unit> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);
        var userId = _httpContextAccessor.HttpContext?.Items["UserId"]?.ToString();
        if (string.IsNullOrEmpty(userId))
            throw new UnauthorizedAccessException("Falha ao obter o ID do usuário.");

        var group =
            await _groupRepository.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException("Grupo não encontrado.");

        if (group.UserId.ToString() != userId)
            throw new UnauthorizedAccessException("Esse grupo não pertence ao usuário logado.");

        if (group.Name == request.Name)
            return Unit.Value;

        var newName = await _groupRepository.GetUserGroupByNameAsync(
            group.UserId,
            request.Name,
            cancellationToken
        );

        if (newName == null)
        {
            _mapper.Map(request, group);
            await _groupRepository.UpdateAsync(group, cancellationToken);
            return Unit.Value;
        }
        else
            throw new InvalidOperationException("Já existe um grupo com esse nome.");
    }
}
