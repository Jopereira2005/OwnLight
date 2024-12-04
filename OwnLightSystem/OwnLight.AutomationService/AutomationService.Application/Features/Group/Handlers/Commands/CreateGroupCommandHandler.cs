using System.Reflection.Metadata;
using AutoMapper;
using AutomationService.Application.Features.Group.Commands;
using AutomationService.Domain.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Entity = AutomationService.Domain.Entities;

namespace AutomationService.Application.Features.Group.Handlers.Commands;

public class CreateGroupCommandHandler(
    IGroupRepository groupRepository,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor,
    IValidator<CreateGroupCommand> validator
) : IRequestHandler<CreateGroupCommand, Guid>
{
    private readonly IGroupRepository _groupRepository = groupRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IValidator<CreateGroupCommand> _validator = validator;

    public async Task<Guid> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);
        var userId = _httpContextAccessor.HttpContext?.Items["UserId"]?.ToString();
        if (string.IsNullOrEmpty(userId))
            throw new UnauthorizedAccessException("Falha ao obter o ID do usuário.");

        if (request.Description == string.Empty)
            request.Description = null;

        var group = _mapper.Map<Entity.Group>(request);
        group.UserId = Guid.Parse(userId);

        await _groupRepository.CreateAsync(group, cancellationToken);
        return group.Id;
    }
}
