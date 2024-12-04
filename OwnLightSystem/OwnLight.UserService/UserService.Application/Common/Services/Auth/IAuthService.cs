using UserService.Domain.Entities;

namespace UserService.Application.Common.Services.Auth;

public interface IAuthService
{
    Task LogoutUserAsync(Guid userId);
    Task<string> LoginUserAsync(User user);
}
