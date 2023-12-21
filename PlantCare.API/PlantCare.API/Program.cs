using System.Net;
using System.Reflection;
using PlantCare.API.DataAccess;
using Serilog;
using MediatR;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using PlantCare.API.Services;
using PlantCare.API.SignalR;
using PlantCare.API.SignalR.Hubs;

const string AllowSpecifiOrigin = "AllowSpecifiOrigin";

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options
        .AddPolicy(name: AllowSpecifiOrigin, policy => policy.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()
    );
});

builder.WebHost.UseKestrel();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.ConfigureSignalRService();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureServices();
builder.Services.SetupDataAccess();
builder.Services.SetupCache();
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblies(typeof(PlantCare.API.Services.Handlers.CreatePlantHandler).GetTypeInfo().Assembly));

var logger = new LoggerConfiguration()
    .ReadFrom
    .Configuration(builder.Configuration)
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

var app = builder.Build();
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

//app.Migrate();  
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(AllowSpecifiOrigin);

app.UseAuthorization();

app.MapControllers();

app.MapHub<CurrentMoistureHub>("current-moisture-level");

app.Run();