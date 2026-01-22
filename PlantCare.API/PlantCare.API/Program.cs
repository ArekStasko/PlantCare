using Coravel;
using IdentityProviderSystem.Client;
using IdentityProviderSystem.Client.Middleware;
using PlantCare.API.Client;
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

var idpLocalUrl = Environment.GetEnvironmentVariable("IdpUrl");
var redisConnectionString =
    $"{Environment.GetEnvironmentVariable("RedisConnectionString")},password={Environment.GetEnvironmentVariable("RedisPassword")}";
var redisInstance = Environment.GetEnvironmentVariable("RedisInstance");

var builder = WebApplication.CreateBuilder(args);

var isSwagger = builder.Environment.IsEnvironment("Swagger");

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowSpecifiOrigin, policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

builder.WebHost.UseKestrel();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();
builder.Services.AddIdpHttpClient(idpLocalUrl);

builder.Services.AddCommandsMapperProfile();
builder.Services.AddQueriesMapperProfile();
builder.Services.AddConsistencyManagerMapperProfile();

builder.Services.ConfigureQueries();
builder.Services.ConfigureCommands();

builder.Services.ConfigureClient();

if (!isSwagger)
{
    builder.Services.AddStackExchangeRedisCache(options =>
    {
        options.Configuration = redisConnectionString;
        options.InstanceName = redisInstance;
    });

    builder.Services.AddMessageBroker();

    builder.Services.AddReadDataManager();
    builder.Services.AddWriteDataManager();

    builder.Services.AddQueueMessageConsumer<HumidityMeasurementConsistencyService, HumidityMeasurement>();
    builder.Services.AddQueueMessageConsumer<ModuleConsistencyService, Module>();
    builder.Services.AddQueueMessageConsumer<PlaceConsistencyService, Place>();
    builder.Services.AddQueueMessageConsumer<PlantConsistencyService, Plant>();
}

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

if (!isSwagger)
{
    app.MigrateReadDatabase();
    app.MigrateWriteDatabase();
    app.UseMiddleware<Authorization>();
}

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