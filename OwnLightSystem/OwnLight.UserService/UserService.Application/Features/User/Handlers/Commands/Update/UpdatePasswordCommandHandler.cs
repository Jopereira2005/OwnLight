using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using UserService.Application.Common.Services.Auth;
using UserService.Application.Common.Services.Messages;
using UserService.Application.Features.User.Commands.Update;
using UserService.Domain.Interfaces;
using Entity = UserService.Domain.Entities;

namespace UserService.Application.Features.User.Handlers.Commands.Update;

public class UpdatePasswordCommandHandler(
    IUserRepository userRepository,
    IRefreshTokenRepository refreshTokenRepository,
    IValidator<UpdatePasswordCommand> validator,
    IMessageService messageService,
    IAuthService authService
) : IRequestHandler<UpdatePasswordCommand, Message>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;
    private readonly IValidator<UpdatePasswordCommand> validator = validator;
    private readonly IMessageService _messageService = messageService;
    private readonly IAuthService _authService = authService;

    public async Task<Message> Handle(
        UpdatePasswordCommand request,
        CancellationToken cancellationToken
    )
    {
        var user = await _userRepository.FindByIdAsync(request.Id);

        if (user == null)
            return _messageService.CreateNotFoundMessage("Usuário não encontrado");

        var userToken = await _refreshTokenRepository.GetUserTokenAsync(user.Id);
        if (userToken == null || userToken.IsRevoked == true)
            return _messageService.CreateNotAuthorizedMessage("Usuário não está logado");

        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return _messageService.CreateValidationMessage(
                validationResult.Errors.Select(e => e.ErrorMessage)
            );

        var passwordHasher = new PasswordHasher<Entity.User>();
        var passwordVerificationResult = passwordHasher.VerifyHashedPassword(
            user,
            user.Password,
            request.CurrentPassword
        );
        if (passwordVerificationResult == PasswordVerificationResult.Failed)
            return _messageService.CreateBadRequestMessage("Senha atual incorreta");

        if (request.CurrentPassword == request.NewPassword)
            return _messageService.CreateConflictMessage(
                "Nova senha não pode ser igual a senha atual"
            );

        request.NewPassword = passwordHasher.HashPassword(user, request.NewPassword);
        await _userRepository.UpdatePasswordAsync(user.Id, request.NewPassword);
        // Logout user after updating user information (bussiness rule)
        await _authService.LogoutUserAsync(user.Id);

        return _messageService.CreateSuccessMessage("Senha atualizada com sucesso");
    }
}
