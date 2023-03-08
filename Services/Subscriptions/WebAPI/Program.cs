using ApplicationCore.Factories;
using ApplicationCore.Jobs;
using ApplicationCore.Jobs.Listeners;
using ApplicationCore.Profiles;
using ApplicationCore.Repositories;
using ApplicationCore.Services;
using ApplicationCore.Services.Contracts;
using Infrastructure.DataContext;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Quartz;
using Quartz.Impl;
using System.Collections.Specialized;
using WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SubscriptionDataContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("Postgresql")));
builder.Services.AddAutoMapper(typeof(AutomapperProfile).Assembly);
builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddTransient<SendEmailJob>();
var quartz = ConfigureQuartz();
builder.Services.AddSingleton(p => quartz);

var serviceName = "Subscriptions Service";
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
    .AddEntityFrameworkCoreInstrumentation()
    .AddNpgsql()
    .AddQuartzInstrumentation()
    .AddHoneycomb(opt =>
    {
        opt.ServiceName = serviceName;
        opt.Dataset = ".net next course";
        opt.ApiKey = builder.Configuration.GetValue<string>("Honeycomb:ApiKey");
    });
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
quartz.JobFactory = new SendEmailJobFactory(builder.Services.BuildServiceProvider());
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UpdateDatabase();

app.Run();

static IScheduler ConfigureQuartz()
{
    var props = new NameValueCollection
            {
                { "quartz.serializer.type", "binary" }
            };
    var factory = new StdSchedulerFactory(props);
    var scheduler = factory.GetScheduler().Result;
    scheduler.Start().Wait();
    scheduler.ListenerManager.AddTriggerListener(new SendEmailTriggerListener());
    scheduler.ListenerManager.AddJobListener(new SendEmailJobListener());
    scheduler.ListenerManager.AddSchedulerListener(new SendEmailSchedulerListener());
    return scheduler;
}
