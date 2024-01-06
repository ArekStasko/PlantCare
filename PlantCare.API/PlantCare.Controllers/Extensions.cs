using System.ComponentModel.DataAnnotations;
using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;

namespace PlantCare.Controllers;

public static class Extensions
{
    public static IActionResult ToOk<TResult>(this Result<TResult> result)
    {
        return result.Match<IActionResult>(
            obj => new OkObjectResult(obj),
            exception =>
            {
                if (exception is ValidationException validationException)
                {
                    return new BadRequestObjectResult(validationException);
                }

                return new StatusCodeResult(500);
            });
    }
}