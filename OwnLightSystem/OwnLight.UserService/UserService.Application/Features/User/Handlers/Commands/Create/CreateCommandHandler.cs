using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using UserService.Application.Common.Services.Messages;
using UserService.Application.Features.User.Commands;
using UserService.Domain.Interfaces;
using Entity = UserService.Domain.Entities;

namespace UserService.Application.Features.User.Handlers.Commands.Create;

public class CreateCommandHandler(
    IMapper mapper,
    IUserRepository userRepository,
    IPasswordHasher<Entity.User> passwordHasher,
    IValidator<CreateCommand> validator,
    IMessageService messageService
) : IRequestHandler<CreateCommand, Message>
{
    private readonly IMapper _mapper = mapper;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordHasher<Entity.User> _passwordHasher = passwordHasher;
    private readonly IValidator<CreateCommand> _validator = validator;
    private readonly IMessageService _messageService = messageService;

    public async Task<Message> Handle(CreateCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return _messageService.CreateValidationMessage(
                validationResult.Errors.Select(e => e.ErrorMessage)
            );

        var existingUser = await _userRepository.FindByUsernameAsync(request.Username);
        if (existingUser != null)
            return _messageService.CreateConflictMessage($"Usuário {request.Username} já existe");

        var existingEmail = await _userRepository.FindByEmailAsync(request.Email);
        if (existingEmail != null)
            return _messageService.CreateConflictMessage("Esse email já está em uso");

        request.Password = _passwordHasher.HashPassword(
            new Entity.User
            {
                Name = request.Name,
                Username = request.Username,
                Email = request.Email,
                Password = request.Password,
            },
            request.Password
        );
        await _userRepository.RegisterAsync(_mapper.Map<Entity.User>(request));

        return _messageService.CreateCreatedMessage("Usuário criado com sucesso");
    }
}
