using InventoryManagementSystem.API.RabbitMQ;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.API.Helpers;
using OrderManagementSystem.DataAccessLayer;
using OrderManagementSystem.Logic;
using OrderManagementSystem.RabbitMqAccessLayer;

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

ConfigurationLoader.LoadConfigurationValue(config, "SqlServer");
ConfigurationLoader.LoadConfigurationValue(config, "RabbitMQHost");
ConfigurationLoader.LoadConfigurationValue(config, "RabbitMQPort");
ConfigurationLoader.LoadConfigurationValue(config, "RabbitMQUser");
ConfigurationLoader.LoadConfigurationValue(config, "RabbitMQPassword");

var connectionString = config["SqlServer"];

builder.Services.AddDbContext<RepositoryContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddHostedService<MessageBusSubscriberOrderApprovedEvent>();
builder.Services.AddHostedService<MessageBusSubscriberOrderDeniedEvent>();

builder.Services.AddScoped<IOrderManager, OrderManager>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
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
