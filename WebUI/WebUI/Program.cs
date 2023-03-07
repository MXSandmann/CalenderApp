using FluentValidation;
using System.Reflection;
using WebUI.Clients;
using WebUI.Clients.Contracts;
using WebUI.Models;
using WebUI.Validators;
using OpenTelemetry.Trace;
using OpenTelemetry.Resources;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IValidator<CreateUpdateUserEventViewModel>, DateValidator>();
builder.Services.AddScoped<IValidator<CreateSubscriptionViewModel>, SubscriptionValidator>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddHttpClient<IEventsClient, EventsClient>((client) =>
    {
        client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("Services:Gateway") + "/e/");
    });
builder.Services.AddHttpClient<ISubscriptionsClient, SubscriptionsClient>((client) =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("Services:Gateway") + "/s/");
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
    .AddSqlClientInstrumentation();
});
builder.Services.AddSingleton(new ActivitySource(serviceName));


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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();