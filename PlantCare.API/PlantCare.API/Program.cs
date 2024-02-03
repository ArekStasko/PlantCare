using System.Net;
using PlantCare.Commands;
using PlantCare.ConsistencyManager;
using PlantCare.Persistance.ReadDataManager;
using PlantCare.Persistance.WriteDataManager;
using PlantCare.Queries;
using Serilog;

const string AllowSpecifiOrigin = "AllowSpecificOrigin";

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
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddConsistencyManagerMapperProfile();
builder.Services.AddConsistencyManager();

builder.Services.AddCommandsMapperProfile();
builder.Services.AddQueriesMapperProfile();

builder.Services.AddReadDataManager();
builder.Services.AddReadCache();

builder.Services.AddWriteDataManager();

builder.Services.ConfigureQueries();
builder.Services.ConfigureCommands();

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

app.MigrateReadDatabase();  
app.MigrateWriteDatabase();  

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
 
app.Run();