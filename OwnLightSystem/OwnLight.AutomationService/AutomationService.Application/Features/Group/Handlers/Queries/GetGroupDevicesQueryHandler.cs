using AutoMapper;
using AutomationService.Application.Contracts.DTOs;
using AutomationService.Application.Features.Group.Queries;
using AutomationService.Domain.Interfaces;
using MediatR;

namespace AutomationService.Application.Features.Group.Handlers.Queries;

public class GetGroupDevicesQueryHandler(IGroupRepository groupRepository, IMapper mapper)
    : IRequestHandler<GetGroupDevicesQuery, GroupDevicesDTO>
{
    private readonly IGroupRepository _groupRepository = groupRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<GroupDevicesDTO> Handle(
        GetGroupDevicesQuery request,
        CancellationToken cancellationToken
    )
    {
        var group =
            await _groupRepository.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException("Grupo não encontrado.");

        var groupDevices = await _groupRepository.GetGroupDevicesAsync(group.Id, cancellationToken);

        var result = _mapper.Map<GroupDevicesDTO>(groupDevices);
        return result;
    }
}
