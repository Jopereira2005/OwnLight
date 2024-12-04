using MediatR;
using UserService.Application.Common.Services.Messages;
using UserService.Application.Features.User.Commands;
using UserService.Domain.Interfaces;

namespace UserService.Application.Features.User.Handlers.Commands.Delete;

public class DeleteCommandHandler(
    IUserRepository userRepository,
    IRefreshTokenRepository refreshTokenRepository,
    IMessageService messageService
) : IRequestHandler<DeleteCommand, Message>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;
    private readonly IMessageService _messageService = messageService;

    public async Task<Message> Handle(DeleteCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindByIdAsync(request.Id);
        if (user == null)
            return _messageService.CreateNotFoundMessage("Usuário não encontrado");

        var userToken = await _refreshTokenRepository.GetUserTokenAsync(user.Id);
        if (userToken == null || userToken.IsRevoked == true)
            return _messageService.CreateNotAuthorizedMessage("Usuário não está logado");

        await _userRepository.DeleteAsync(request.Id);

        return _messageService.CreateSuccessMessage("Usuário deletado com sucesso");
    }
}
