using Microsoft.AspNetCore.Mvc;

namespace PlantCare.API.Controllers;

public class ControllerAuth : ControllerBase
{
    protected int UserId { get; private set; }
    
    public ControllerAuth(IHttpContextAccessor httpContextAccessor)
    {
        string secretToken = Environment.GetEnvironmentVariable("secretToken");

        var authHeader = httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
        if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
        {
            var token = authHeader.Substring("Bearer ".Length).Trim();
            if(token == secretToken)
            {
                return;
            }
        }
        
        if (httpContextAccessor.HttpContext.Items.TryGetValue("UserId", out var userId))
        {
            UserId = (int)userId;
            return;
        }
        
        throw new UnauthorizedAccessException();
    }
}