using MediatR;
using UserService.Application.DTOs;

namespace UserService.Application.Features.User.Queries;

public class GetByUsernameQuery(string username) : IRequest<UserResponseDTO>
{
    public string Username { get; set; } = username;
}
