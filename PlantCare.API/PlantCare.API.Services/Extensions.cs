using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using PlantCare.API.Services.Requests;

namespace PlantCare.API.Services;

public static class Extensions
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddMappings();
    }

    private static void AddMappings(this IServiceCollection services) => services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
}