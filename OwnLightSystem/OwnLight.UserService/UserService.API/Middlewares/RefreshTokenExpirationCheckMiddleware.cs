using UserService.Domain.Interfaces;

namespace UserService.API.Middlewares;

public class RefreshTokenExpirationCheckMiddleware(
    RequestDelegate next,
    ILogger<RefreshTokenExpirationCheckMiddleware> logger
)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<RefreshTokenExpirationCheckMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
    {
        var refreshToken = context.Request.Cookies["RefreshToken"];

        if (!string.IsNullOrEmpty(refreshToken))
        {
            _logger.LogInformation(
                "Refresh token encontrado no cookie: {RefreshToken}",
                refreshToken
            );

            using var scope = serviceProvider.CreateScope();
            var refreshTokenRepository =
                scope.ServiceProvider.GetRequiredService<IRefreshTokenRepository>();
            var tokenInDb = await refreshTokenRepository.GetTokenAsync(refreshToken);

            if (tokenInDb != null)
            {
                _logger.LogInformation(
                    "Token encontrado no banco de dados para o usuário {UserId}.",
                    tokenInDb.UserId
                );

                if (tokenInDb.IsRevoked)
                {
                    _logger.LogWarning(
                        "O token já foi revogado para o usuário {UserId}. Deletando cookie.",
                        tokenInDb.UserId
                    );
                    context.Response.Cookies.Delete("RefreshToken");
                }
                else
                {
                    if (tokenInDb.ExpiresAt <= DateTime.UtcNow)
                    {
                        _logger.LogWarning(
                            "O token expirou para o usuário {UserId}, revogando token e deletando cookie.",
                            tokenInDb.UserId
                        );
                        await refreshTokenRepository.RevokeTokenAsync(tokenInDb);
                        context.Response.Cookies.Delete("RefreshToken");
                        _logger.LogInformation("Token de refresh revogado e cookie removido.");
                    }
                    else
                        _logger.LogInformation("O token ainda é válido.");
                }
            }
            else
            {
                _logger.LogWarning(
                    "Nenhum token encontrado no banco de dados para o refresh token: {RefreshToken}. Deletando cookie.",
                    refreshToken
                );
                context.Response.Cookies.Delete("RefreshToken");
            }
        }
        else
            _logger.LogInformation("Nenhum refresh token encontrado no cookie.");

        await _next(context);
    }
}
