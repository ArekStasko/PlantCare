using Microsoft.AspNetCore.Mvc;

namespace PlantCare.API.Controllers;

public class ControllerAuth : ControllerBase
{
    protected int UserId { get; private set; }
    private readonly ILogger<ControllerAuth> _logger;

    public ControllerAuth(ILogger<ControllerAuth> logger)
    {
        _logger = logger;
    }
    
    public ControllerAuth(IHttpContextAccessor httpContextAccessor)
    {
        string secretToken = Environment.GetEnvironmentVariable("secretToken");

        var authHeader = httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
        if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
        {
            var token = authHeader.Substring("Bearer ".Length).Trim();
            _logger.LogInformation("Request contains bearer token : {token} - {authHeader}", token, authHeader);
            if(token == secretToken)
            {
                _logger.LogInformation("Request authorized with secret token");
                return;
            }
        }
        
        if (httpContextAccessor.HttpContext.Items.TryGetValue("UserId", out var userId))
        {
            _logger.LogInformation("Request authorized with IDP");
            UserId = (int)userId;
            return;
        }
        
        _logger.LogInformation("Request is not authorized");
        throw new UnauthorizedAccessException();
    }
}