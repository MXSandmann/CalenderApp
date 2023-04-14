using ApplicationCore.Options;
using ApplicationCore.Profiles;
using ApplicationCore.Providers;
using ApplicationCore.Providers.Contracts;
using ApplicationCore.Repositories.Contracts;
using ApplicationCore.Services;
using ApplicationCore.Services.Contracts;
using Infrastructure.DataContext;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Diagnostics;
using WebAPI.Extensions;
using WebUI.Services;
using WebUI.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<AuthenticationOptions>(builder.Configuration.GetSection("Authentication"));
builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddDbContext<UserDataContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("Postgresql")));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddAutoMapper(typeof(AutomapperProfile).Assembly);
var serviceName = "Authentication Service";
var serviceVersion = "1.0.0";

builder.Services.AddOpenTelemetry().WithTracing(tracerProviderBuilder =>
{
    tracerProviderBuilder
    //.AddConsoleExporter()
    .AddSource(serviceName)
    .SetResourceBuilder(ResourceBuilder.CreateDefault()
        .AddService(serviceName: serviceName, serviceVersion: serviceVersion))
    .AddHttpClientInstrumentation()
    .AddAspNetCoreInstrumentation()
    .AddSqlClientInstrumentation()
    .AddEntityFrameworkCoreInstrumentation()
    .AddNpgsql()
    .AddHoneycomb(opt =>
    {
        opt.ServiceName = serviceName;
        opt.Dataset = ".net next course";
        opt.ApiKey = builder.Configuration.GetValue<string>("Honeycomb:ApiKey");
    });
});
builder.Services.AddSingleton(new ActivitySource(serviceName));
builder.Services.AddTransient<IPasswordHasher, Sha512PasswordHasher>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.UpdateDatabase(builder.Configuration);

app.Run();
