using Microsoft.AspNetCore.Http;
using UserService.Application.Common.Services.Token;
using UserService.Domain.Entities;
using UserService.Domain.Interfaces;

namespace UserService.Application.Common.Services.Auth;

public class AuthService(
    ITokenService tokenService,
    IHttpContextAccessor httpContextAccessor,
    IRefreshTokenRepository refreshTokenRepository
) : IAuthService
{
    private readonly ITokenService _tokenService = tokenService;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;

    public async Task LogoutUserAsync(Guid userId)
    {
        var refreshToken = _httpContextAccessor.HttpContext.Request.Cookies["RefreshToken"];

        if (!string.IsNullOrEmpty(refreshToken))
        {
            var tokenInDb = await _refreshTokenRepository.GetTokenAsync(refreshToken);
            if (tokenInDb != null && tokenInDb.UserId == userId && tokenInDb.IsRevoked == false)
                await _refreshTokenRepository.RevokeTokenAsync(tokenInDb);

            _httpContextAccessor.HttpContext.Response.Cookies.Delete("RefreshToken");
        }
    }

    public async Task<string> LoginUserAsync(User user)
    {
        var accessToken = _tokenService.GenerateToken(user);
        var refreshToken = new RefreshToken
        {
            UserId = user.Id,
            Token = _tokenService.GenerateRefreshToken(),
            ExpiresAt = DateTime.UtcNow.AddDays(7),
        };

        await _refreshTokenRepository.CreateAsync(refreshToken);

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = refreshToken.ExpiresAt,
            Secure = true,
        };
        _httpContextAccessor.HttpContext.Response.Cookies.Append(
            "RefreshToken",
            refreshToken.Token,
            cookieOptions
        );

        return accessToken;
    }
}
