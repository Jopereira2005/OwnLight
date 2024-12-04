using AutomationService.Application.Contracts.DTOs;
using MediatR;

namespace AutomationService.Application.Features.Routine.Queries;

public class GetRoutineByNameQuery : IRequest<RoutineResponseDTO>
{
    public required string Name { get; set; }
}
