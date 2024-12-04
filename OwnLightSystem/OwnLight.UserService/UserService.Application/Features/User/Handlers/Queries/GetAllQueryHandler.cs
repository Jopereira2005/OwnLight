using AutoMapper;
using MediatR;
using UserService.Application.DTOs;
using UserService.Application.Features.User.Queries;
using UserService.Domain.Interfaces;

namespace UserService.Application.Features.User.Handlers.Queries;

public class GetAllQueryHandler(IUserRepository userRepository, IMapper mapper)
    : IRequestHandler<GetAllQuery, PaginatedResultDTO>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<PaginatedResultDTO> Handle(
        GetAllQuery request,
        CancellationToken cancellationToken
    )
    {
        var users = await _userRepository.FindAllAsync(request.Page, request.PageSize);
        var totalCount = await _userRepository.CountAsync();
        var usersDTO = _mapper.Map<IEnumerable<UserResponseDTO>>(users);
        return new PaginatedResultDTO(usersDTO, totalCount, request.Page, request.PageSize);
    }
}
