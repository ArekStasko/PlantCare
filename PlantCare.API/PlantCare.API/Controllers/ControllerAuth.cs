using Microsoft.AspNetCore.Mvc;

namespace PlantCare.API.Controllers;

public class ControllerAuth : ControllerBase
{
    protected int UserId { get; private set; }
    
    public ControllerAuth(IHttpContextAccessor httpContextAccessor)
    {
        if (httpContextAccessor.HttpContext.Items.TryGetValue("UserId", out var userId))
        {
            UserId = (int)userId;
        }
        else
        {
            throw new UnauthorizedAccessException();
        }
    }
}