using InventoryManagementSystem.API.Helpers;
using InventoryManagementSystem.API.RabbitMQ;
using InventoryManagementSystem.DataAccessLayer;
using InventoryManagementSystem.Logic;
using InventoryManagementSystem.RabbitMQAccessLayer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.Configure<IISOptions>(options =>
{

});

ConfigurationManager config = builder.Configuration;

ConfigurationLoader.LoadConfigurationValue(config, "StorageType");

ConfigurationLoader.LoadConfigurationValue(config, "RabbitMQHost");
ConfigurationLoader.LoadConfigurationValue(config, "RabbitMQPort");
ConfigurationLoader.LoadConfigurationValue(config, "RabbitMQUser");
ConfigurationLoader.LoadConfigurationValue(config, "RabbitMQPassword");

Console.WriteLine(config["RabbitMQHost"]);
Console.WriteLine(config["RabbitMQPort"]);
Console.WriteLine(config["RabbitMQUser"]);
Console.WriteLine(config["RabbitMQPassword"]);

string connectionString;

switch (config["StorageType"])
{
    case "SqlServer":
        ConfigurationLoader.LoadConfigurationValue(config, "SqlServer");
        connectionString = config["SqlServer"];
        builder.Services.AddDbContext<RepositoryContext>(options => options.UseSqlServer(connectionString));
        break;

    case "CosmosDb":
        ConfigurationLoader.LoadConfigurationValue(config, "CosmosDb");
        connectionString = config["CosmosDb"];
        builder.Services.AddDbContext<RepositoryContext>(options => options.UseCosmos(connectionString, "ESES5_InventoryDb"));
        break;

    default:
        builder.Services.AddDbContext<RepositoryContext>(options => options.UseInMemoryDatabase("ESES5_InventoryDb"));
        break;
}


builder.Services.AddHostedService<MessageBusSubscriberDecreaseStockEvent>();
builder.Services.AddHostedService<MessageBusSubscriberProductCreatedEvent>();
builder.Services.AddHostedService<MessageBusSubscriberProductDeletedEvent>();
builder.Services.AddHostedService<MessageBusSubscriberProductUpdatedEvent>();

builder.Services.AddScoped<IInventoryManager, InventoryManager>();
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<IMessageBusClient, MessageBusClient>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseForwardedHeaders(new ForwardedHeadersOptions()
{
    ForwardedHeaders = ForwardedHeaders.All
});

app.UseRouting();

app.UseCors("CorsPolicy");


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
