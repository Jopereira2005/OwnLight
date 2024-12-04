using UserService.Domain.Entities;

namespace UserService.Domain.Interfaces;

public interface IRefreshTokenRepository
{
    Task<RefreshToken> CreateAsync(RefreshToken refreshToken);
    Task<RefreshToken?> GetTokenAsync(string refreshToken);
    Task<RefreshToken?> GetUserTokenAsync(Guid userId);
    Task RevokeTokenAsync(RefreshToken refreshToken);
    Task DeleteAllTokens();
}
    