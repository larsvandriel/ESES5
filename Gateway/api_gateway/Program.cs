using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;


builder.Configuration.AddJsonFile("ocelot.json");

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var JwtSecret = configuration["JWT:Secret"];

if (builder.Environment.IsProduction())
{
    JwtSecret = JwtSecret.Replace("{JwtSecret}", Environment.GetEnvironmentVariable("JWT_SECRET"));
    configuration["JWT:Secret"] = JwtSecret;
}

builder.Services.AddAuthentication().AddJwtBearer("TestKey", x =>
{
    x.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateAudience=false,
        ValidateIssuer=false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSecret))
    };
});

builder.Services.AddOcelot();

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

app.UseCors("CorsPolicy");

app.UseOcelot();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
