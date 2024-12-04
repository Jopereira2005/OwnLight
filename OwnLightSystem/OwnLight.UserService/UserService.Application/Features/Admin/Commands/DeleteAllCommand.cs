using MediatR;
using Entity = UserService.Domain.Entities;

namespace UserService.Application.Features.Admin.Commands;

public class DeleteAllCommand : IRequest
{
    public Guid AdminId { get; set; }
    public required string AdminPassword { get; set; }
}
