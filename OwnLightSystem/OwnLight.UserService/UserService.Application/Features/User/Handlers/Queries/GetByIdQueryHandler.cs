using AutoMapper;
using MediatR;
using UserService.Application.DTOs;
using UserService.Application.Features.User.Queries;
using UserService.Domain.Interfaces;

namespace UserService.Application.Features.User.Handlers.Queries;

public class GetByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
    : IRequestHandler<GetByIdQuery, AdminResponseDTO>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<AdminResponseDTO> Handle(
        GetByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        var user =
            await _userRepository.FindByIdAsync(request.Id)
            ?? throw new DllNotFoundException("Usuário não encontrado");
        return _mapper.Map<AdminResponseDTO>(user);
    }
}
