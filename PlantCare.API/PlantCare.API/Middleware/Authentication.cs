using IdentityProviderSystem.Client.Services;

namespace PlantCare.API.Middleware;

public class Authentication
{
    private readonly RequestDelegate _next;
    private readonly ITokenService _tokenService;

    public Authentication(RequestDelegate next, ITokenService tokenService)
    {
        _next = next;
        _tokenService = tokenService;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var authorizationHeader = context.Request.Headers["Authorization"].ToString();
        if (string.IsNullOrEmpty(authorizationHeader))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return;
        }
        var token = authorizationHeader.Substring("Bearer ".Length).Trim();
        var tokenValidationResult = await _tokenService.ValidateToken(token);
        if (!tokenValidationResult.IsTokenValid)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return;
        }
        context.Items["UserId"] = tokenValidationResult.UserId;
        await _next(context);
    }
}