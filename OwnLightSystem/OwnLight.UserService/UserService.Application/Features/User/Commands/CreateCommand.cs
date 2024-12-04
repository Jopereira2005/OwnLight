using MediatR;
using UserService.Application.Common.Services.Messages;

namespace UserService.Application.Features.User.Commands;

public class CreateCommand(string name, string username, string email, string password)
    : IRequest<Message>
{
    public string Name { get; set; } = name;
    public string Username { get; set; } = username;
    public string Email { get; set; } = email;
    public string Password { get; set; } = password;
}
