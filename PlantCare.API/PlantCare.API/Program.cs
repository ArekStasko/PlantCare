using IdentityProviderSystem.Client;
using IdentityProviderSystem.Client.Middleware;
using PlantCare.Commands;
using PlantCare.ConsistencyManager;
using PlantCare.ConsistencyManager.Services;
using PlantCare.MessageBroker;
using PlantCare.MessageBroker.Messages;
using PlantCare.Persistance.ReadDataManager;
using PlantCare.Persistance.WriteDataManager;
using PlantCare.Queries;
using Serilog;

const string AllowSpecifiOrigin = "AllowSpecificOrigin";
string IdpLocalUrl = Environment.GetEnvironmentVariable("IdpUrl");
var redisConnectionString = $"{Environment.GetEnvironmentVariable("RedisConnectionString")},password={Environment.GetEnvironmentVariable("RedisPassword")}";
var redisInstance = Environment.GetEnvironmentVariable("RedisInstance");

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

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = redisConnectionString;
    options.InstanceName = redisInstance;
});

builder.Services.AddMessageBroker();

builder.Services.AddHttpContextAccessor();
builder.Services.AddIdpHttpClient(IdpLocalUrl);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCommandsMapperProfile();
builder.Services.AddQueriesMapperProfile();

builder.Services.AddReadDataManager();

builder.Services.AddWriteDataManager();

builder.Services.ConfigureQueries();
builder.Services.ConfigureCommands();

builder.Services.AddConsistencyManagerMapperProfile();

builder.Services.AddQueueMessageConsumer<HumidityMeasurementConsistencyService, HumidityMeasurement>();
builder.Services.AddQueueMessageConsumer<ModuleConsistencyService, Module>();
builder.Services.AddQueueMessageConsumer<PlaceConsistencyService, Place>();
builder.Services.AddQueueMessageConsumer<PlantConsistencyService, Plant>();

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

app.UseMiddleware<Authorization>();

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