using System.IdentityModel.Tokens.Jwt;
using System.Net;

namespace AutomationService.API.Middlewares;

public class JwtMiddleware(RequestDelegate next, ILogger<JwtMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<JwtMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        var token = context.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();

        if (!string.IsNullOrEmpty(token))
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();

                if (handler.ReadToken(token) is JwtSecurityToken jwtToken)
                {
                    var userId = jwtToken
                        .Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub)
                        ?.Value;
                    if (userId != null)
                    {
                        context.Items["UserId"] = userId;
                        context.Items["JwtToken"] = token;

                        _logger.LogInformation("User ID: {UserId}", userId);
                        _logger.LogInformation("Token: {Token}", token);
                    }
                }
                _logger.LogInformation("Token is valid.");
            }
            catch (Exception)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync("Invalid token.");
                return;
            }
        }
        await _next(context);
    }
}
