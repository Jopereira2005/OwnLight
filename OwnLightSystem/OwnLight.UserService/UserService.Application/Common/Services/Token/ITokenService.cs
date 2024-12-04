using System.Security.Claims;
using UserService.Domain.Entities;

namespace UserService.Application.Common.Services.Token;

public interface ITokenService
{
    public string GenerateToken(User user);
    public string GenerateRefreshToken();
    public ClaimsPrincipal ValidateToken(string token);
}
