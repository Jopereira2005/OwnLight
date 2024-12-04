using MediatR;
using UserService.Application.Common.Services.Messages;

namespace UserService.Application.Features.User.Commands;

public class DeleteCommand(Guid id) : IRequest<Message>
{
    public Guid Id { get; set; } = id;
}
