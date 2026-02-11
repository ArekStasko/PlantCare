using Microsoft.AspNetCore.Mvc;

namespace PlantCare.API.Controllers;

public class ControllerAuth : ControllerBase
{
    protected int UserId { get; private set; }
    
    public ControllerAuth(IHttpContextAccessor httpContextAccessor, ILogger<ControllerAuth> logger)
    {
        string secretToken = Environment.GetEnvironmentVariable("secretToken");

        var authHeader = httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
        if (!string.IsNullOrEmpty(authHeader))
        {
            var headerParts = authHeader.Split(' ');
            if(headerParts.Length == 2 && headerParts[1] == secretToken)
            {
                logger.LogInformation($"Flow authorized by secret token");
                return;
            }
        }
        
        if (httpContextAccessor.HttpContext.Items.TryGetValue("UserId", out var userId))
        {
            UserId = (int)userId;
            return;
        }
        
        throw new UnauthorizedAccessException("User is not authorized on controller base ");
    }
}