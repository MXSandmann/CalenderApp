using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Reflection;
using WebUI.Clients;
using WebUI.Clients.Contracts;
using WebUI.Jwt;
using WebUI.Jwt.Contracts;
using WebUI.Models.ViewModels;
using WebUI.Validators;
using AuthenticationOptions = WebUI.Options.AuthenticationOptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IValidator<CreateUpdateUserEventViewModel>, DateValidator>();
builder.Services.AddScoped<IValidator<CreateSubscriptionViewModel>, SubscriptionValidator>();
builder.Services.AddScoped<IValidator<RegisterViewModel>, RegisterValidator>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddHttpClient<IEventsClient, EventsClient>((provider, client) =>
    {
        var context = provider.GetRequiredService<IHttpContextAccessor>().HttpContext;
        var token = context?.User.FindFirst("Jwt")?.Value ?? string.Empty;
                
        client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("Services:Gateway") + "/e/");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //client.BaseAddress = new Uri("http://localhost:5154/api/");
    });
builder.Services.AddHttpClient<ISubscriptionsClient, SubscriptionsClient>((provider, client) =>
    {
        var context = provider.GetRequiredService<IHttpContextAccessor>().HttpContext;
        var token = context?.User.FindFirst("Jwt")?.Value ?? string.Empty;
        client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("Services:Gateway") + "/s/");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    });
builder.Services.AddHttpClient<IAuthenticationClient, AuthenticationClient>((client) =>
    {
        client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("Services:Gateway") + "/au/");
        //client.BaseAddress = new Uri("http://localhost:5193/api/account/");
    });

var serviceName = "EventingWebsite";
var serviceVersion = "1.0.0";

builder.Services.AddOpenTelemetry().WithTracing(tracerProviderBuilder =>
{
    tracerProviderBuilder   
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
builder.Services.AddHttpContextAccessor();


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