using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Diagnostics;
using System.Reflection;
using WebUI.Clients;
using WebUI.Clients.Contracts;
using WebUI.Jwt;
using WebUI.Jwt.Contracts;
using WebUI.Models.ViewModels;
using WebUI.Options;
using WebUI.Services;
using WebUI.Services.Contracts;
using WebUI.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IValidator<CreateUpdateUserEventViewModel>, DateValidator>();
builder.Services.AddScoped<IValidator<CreateSubscriptionViewModel>, SubscriptionValidator>();
builder.Services.AddScoped<IValidator<RegisterViewModel>, RegisterValidator>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddHttpClient<IEventsClient, EventsClient>((client) =>
    {
        client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("Services:Gateway") + "/e/");
    });
builder.Services.AddHttpClient<ISubscriptionsClient, SubscriptionsClient>((client) =>
    {
        client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("Services:Gateway") + "/s/");
    });
builder.Services.AddHttpClient<IAuthenticationClient, AuthenticationClient>((client) =>
    {
        client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("Services:Gateway") + "/au/");
        //client.BaseAddress = new Uri("http://localhost:5193/api/");

    });

var serviceName = "EventingWebsite";
var serviceVersion = "1.0.0";

builder.Services.AddOpenTelemetry().WithTracing(tracerProviderBuilder =>
{
    tracerProviderBuilder
    .AddConsoleExporter()
    .AddSource(serviceName)
    .SetResourceBuilder(ResourceBuilder.CreateDefault()
        .AddService(serviceName: serviceName, serviceVersion: serviceVersion))
    .AddHttpClientInstrumentation()
    .AddAspNetCoreInstrumentation()
    .AddSqlClientInstrumentation()
    .AddHoneycomb(opt =>
    {
        opt.ServiceName = serviceName;
        opt.Dataset = ".net next course";
        opt.ApiKey = builder.Configuration.GetValue<string>("Honeycomb:ApiKey");
    });
});
builder.Services.AddSingleton(new ActivitySource(serviceName));
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();
builder.Services.Configure<AuthenticationOptions>(builder.Configuration.GetSection("Authentication"));
builder.Services.AddScoped<IJwtValidator, JwtValidator>();
builder.Services.AddTransient<IPasswordHasher, Sha512PasswordHasher>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();