using MediatR;
using Microsoft.AspNetCore.Http;
using UserService.Application.Common.Services.Messages;
using UserService.Application.Common.Services.Token;
using UserService.Application.Features.Authentication.Command;
using UserService.Domain.Interfaces;

namespace UserService.Application.Features.Authentication.Handlers;

public class RefreshTokenCommandHandler(
    IRefreshTokenRepository refreshTokenRepository,
    IUserRepository userRepository,
    ITokenService tokenService,
    IMessageService messageService
) : IRequestHandler<RefreshTokenCommand, Message>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IMessageService _messageService = messageService;

    public async Task<Message> Handle(
        RefreshTokenCommand request,
        CancellationToken cancellationToken
    )
    {
        var user = await _userRepository.FindByIdAsync(request.UserId);
        
        if (user == null)
            return _messageService.CreateNotFoundMessage("Usuário não encontrado.");

        var tokenInDb = await _refreshTokenRepository.GetUserTokenAsync(user.Id);

        if (tokenInDb == null || tokenInDb.IsExpired() || tokenInDb.IsRevoked == true)
            return _messageService.CreateNotAuthorizedMessage("Token inválido.");

        var newAccessToken = _tokenService.GenerateToken(user);

        return _messageService.CreateRefreshMessage(
            "Access token atualizado com sucesso.",
            newAccessToken
        );
    }
}
