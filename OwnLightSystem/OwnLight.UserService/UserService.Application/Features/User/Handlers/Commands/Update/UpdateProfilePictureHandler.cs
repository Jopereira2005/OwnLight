using AutoMapper;
using FluentValidation;
using MediatR;
using UserService.Application.Common.Services.Messages;
using UserService.Application.Features.User.Commands.Update;
using UserService.Domain.Interfaces;

namespace UserService.Application.Features.User.Handlers.Commands.Update;

public class UpdateProfilePictureHandler(
    IUserRepository userRepository,
    IRefreshTokenRepository refreshTokenRepository,
    IValidator<UpdateProfilePictureCommand> validator,
    IMessageService messageService,
    IMapper mapper
) : IRequestHandler<UpdateProfilePictureCommand, Message>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;
    private readonly IValidator<UpdateProfilePictureCommand> _validator = validator;
    private readonly IMessageService _messageService = messageService;
    private readonly IMapper _mapper = mapper;

    public async Task<Message> Handle(
        UpdateProfilePictureCommand request,
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

        user.UpdatedAt = DateTime.UtcNow;
        _mapper.Map(request, user);

        await _userRepository.UpdateAsync(user);

        return _messageService.CreateSuccessMessage("Foto de perfil atualizada com sucesso");
    }
}
