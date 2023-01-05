using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.API.Helpers;
using ProductManagementSystem.API.RabbitMQ;
using ProductManagementSystem.DataAccessLayer;
using ProductManagementSystem.Logic;
using ProductManagementSystem.RabbitMqAccessLayer;

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

ConfigurationLoader.LoadConfigurationValue(config, "Swagger");
ConfigurationLoader.LoadConfigurationValue(config, "StorageType");

ConfigurationLoader.LoadConfigurationValue(config, "RabbitMQHost");
ConfigurationLoader.LoadConfigurationValue(config, "RabbitMQPort");
ConfigurationLoader.LoadConfigurationValue(config, "RabbitMQUser");
ConfigurationLoader.LoadConfigurationValue(config, "RabbitMQPassword");

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

        builder.Services.AddDbContext<RepositoryContext>(options => options.UseCosmos(connectionString, "ESES5_ProductDb"));
        break;

    default:
        builder.Services.AddDbContext<RepositoryContext>(options => options.UseInMemoryDatabase("ESES5_ProductDb"));
        break;
}



builder.Services.AddHostedService<MessageBusSubscriberUpdateStockEvent>();

builder.Services.AddScoped<IProductManager, ProductManager>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IMessageBusClient, MessageBusClient>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || config["Swagger"].Equals("Enabled"))
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
