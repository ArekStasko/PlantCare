using IdentityProviderSystem.Client.Services;

namespace PlantCare.API.Middleware;

public class Authorization
{
    private readonly RequestDelegate _next;
    private readonly ITokenService _tokenService;
    private ILogger<Authorization> _logger;

    public Authorization(RequestDelegate next, ITokenService tokenService, ILogger<Authorization> logger)
    {
        _next = next;
        _tokenService = tokenService;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].ToString();
        if (string.IsNullOrEmpty(token))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            _logger.LogWarning("There is no authorizationHeader in the request");
            return;
        }
        var tokenValidationResult = await _tokenService.ValidateToken(token);
        _logger.LogWarning($"TOKEN : {token}");
        _logger.LogWarning($"TOKEN VALIDATION RESULT: {tokenValidationResult.UserId} - {tokenValidationResult.IsTokenValid}");
        if (!tokenValidationResult.IsTokenValid)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            _logger.LogWarning("User is no authorized");
            return;
        }
        
        context.Items["UserId"] = tokenValidationResult.UserId;
        await _next(context);
    }
}