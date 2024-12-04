using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using UserService.Application.Common.Services.Auth;
using UserService.Application.Common.Services.Messages;
using UserService.Application.Features.User.Commands.Update;
using UserService.Domain.Interfaces;

namespace UserService.Application.Features.User.Handlers.Commands.Update;

public class UpdateUsernameCommandHandler(
    IUserRepository userRepository,
    IRefreshTokenRepository refreshTokenRepository,
    IMessageService messageService,
    IValidator<UpdateUsernameCommand> validator,
    IAuthService authService,
    IMapper mapper
) : IRequestHandler<UpdateUsernameCommand, Message>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;
    private readonly IMessageService _messageService = messageService;
    private readonly IValidator<UpdateUsernameCommand> _validator = validator;
    private readonly IAuthService _authService = authService;
    private readonly IMapper _mapper = mapper;

    public async Task<Message> Handle(
        UpdateUsernameCommand request,
        CancellationToken cancellationToken
    )
    {
        var user = await _userRepository.FindByIdAsync(request.Id);

        if (user == null)
            return _messageService.CreateNotFoundMessage("Usuário não encontrado");

        var userToken = await _refreshTokenRepository.GetUserTokenAsync(user.Id);
        if (userToken == null || userToken.IsRevoked == true)
            return _messageService.CreateNotAuthorizedMessage("Usuário não está logado");

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return _messageService.CreateValidationMessage(
                validationResult.Errors.Select(e => e.ErrorMessage)
            );

        var existingUsername = await _userRepository.FindByUsernameAsync(request.Username);
        if (existingUsername != null && request.Username == user.Username)
            return _messageService.CreateConflictMessage($"{request.Username} já existe");

        user.UpdatedAt = DateTime.UtcNow;
        _mapper.Map(request, user);

        await _userRepository.UpdateAsync(user);
        // Logout user after updating user information (bussiness rule)
        await _authService.LogoutUserAsync(user.Id);

        return _messageService.CreateSuccessMessage("Nome de usuário atualizado com sucesso");
    }
}
