using MediatR;
using Microsoft.AspNetCore.Identity;
using UserService.Application.Features.Admin.Commands;
using UserService.Domain.Interfaces;
using Entity = UserService.Domain.Entities;

namespace UserService.Application.Features.Admin.Handlers;

public class DeleteAllCommandHandler(
    IUserRepository userRepository,
    IAdminRepository adminRepository,
    IPasswordHasher<Entity.User> passwordHasher
) : IRequestHandler<DeleteAllCommand>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IAdminRepository _adminRepository = adminRepository;
    private readonly IPasswordHasher<Entity.User> _passwordHasher = passwordHasher;

    public async Task<Unit> Handle(DeleteAllCommand request, CancellationToken cancellationToken)
    {
        var admin = await _userRepository.FindByIdAsync(request.AdminId);
        if (
            admin == null
            || _passwordHasher.VerifyHashedPassword(admin, admin.Password, request.AdminPassword)
                != PasswordVerificationResult.Success
        )
            throw new UnauthorizedAccessException("Invalid admin credentials");

        await _adminRepository.DeleteAllAsync();
        return Unit.Value;
    }
}
