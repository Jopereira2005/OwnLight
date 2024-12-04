using System;
using MediatR;
using UserService.Application.Common.Services.Messages;

namespace UserService.Application.Features.Authentication.Command;

public class LoginCommand(string username, string password) : IRequest<Message>
{
    public string Username { get; set; } = username;
    public string Password { get; set; } = password;
}
