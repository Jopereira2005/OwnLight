using AutomationService.Application.Contracts.DTOs;
using MediatR;

namespace AutomationService.Application.Features.Group.Queries;

public class GetGroupDevicesQuery(Guid id) : IRequest<GroupDevicesDTO>
{
    public Guid Id { get; set; } = id;
}
