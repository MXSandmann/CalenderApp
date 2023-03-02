using ApplicationCore.Extensions;
using ApplicationCore.Jobs.Listeners;
using ApplicationCore.Profiles;
using ApplicationCore.Repositories;
using ApplicationCore.Services;
using ApplicationCore.Services.Contracts;
using Infrastructure.DataContext;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Quartz.Impl;
using Quartz;
using System.Collections.Specialized;
using ApplicationCore.Factories;
using ApplicationCore.Jobs;
using WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SubscriptionDataContext>(opt => opt.UseNpgsql(builder.Configuration.GetValue<string>("ConnectionString")));
builder.Services.AddAutoMapper(typeof(AutomapperProfile).Assembly);
builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddTransient<SendEmailJob>();
var quartz = ConfigureQuartz();
builder.Services.AddSingleton(p => quartz);
//builder.Services.AddQuartzScheduler();
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
