using AutomationService.Application.Contracts.DTOs;
using MediatR;

namespace AutomationService.Application.Features.Group.Queries;

public class GetUserGroupByNameQuery(string name) : IRequest<GroupResponseDTO>
{
    public string Name { get; set; } = name;
}
