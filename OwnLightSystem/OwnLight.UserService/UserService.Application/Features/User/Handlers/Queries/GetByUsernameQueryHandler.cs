using AutoMapper;
using MediatR;
using UserService.Application.DTOs;
using UserService.Application.Features.User.Queries;
using UserService.Domain.Interfaces;

namespace UserService.Application.Features.User.Handlers.Queries;

public class GetByUsernameQueryHandler(IUserRepository userRepository, IMapper mapper)
    : IRequestHandler<GetByUsernameQuery, UserResponseDTO>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<UserResponseDTO> Handle(
        GetByUsernameQuery request,
        CancellationToken cancellationToken
    )
    {
        var user =
            await _userRepository.FindByUsernameAsync(request.Username)
            ?? throw new Exception("Usuário não encontrado");
        return _mapper.Map<UserResponseDTO>(user);
    }
}
