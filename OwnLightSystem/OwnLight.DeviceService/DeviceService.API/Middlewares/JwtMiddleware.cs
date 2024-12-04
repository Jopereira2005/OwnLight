using System.IdentityModel.Tokens.Jwt;
using System.Net;

namespace DeviceService.API.Middlewares;

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
                // Valida e define o usuário no contexto
                var handler = new JwtSecurityTokenHandler();

                if (handler.ReadToken(token) is JwtSecurityToken jwtToken)
                {
                    var userId = jwtToken
                        .Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub)
                        ?.Value;
                    if (userId != null)
                        context.Items["UserId"] = userId;
                }
            }
            catch (Exception)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await response.WriteAsync("Invalid token.");
                return;
            }
        }

        await _next(context);
    }
}
