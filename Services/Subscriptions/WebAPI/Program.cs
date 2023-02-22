using ApplicationCore.Jobs;
using ApplicationCore.Profiles;
using ApplicationCore.Repositories;
using ApplicationCore.Services;
using ApplicationCore.Services.Contracts;
using Infrastructure.DataContext;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Quartz.Impl;
using System.Collections.Specialized;

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
//builder.Services.AddQuartz(q => q.UseMicrosoftDependencyInjectionJobFactory());
//builder.Services.AddQuartzServer(opt => opt.WaitForJobsToComplete = true);
var quartzScheduler = CofigureQuartz();
builder.Services.AddSingleton(p => quartzScheduler);
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

app.Run();

IScheduler CofigureQuartz()
{
    var props = new NameValueCollection
    {
        {"quartz.serializer.type", "binary" }
    };
    var factory = new StdSchedulerFactory(props);
    var scheduler = factory.GetScheduler().Result;
    scheduler.Start().Wait();
    scheduler.ListenerManager.AddTriggerListener(new SendEmailTriggerListener());
    scheduler.ListenerManager.AddJobListener(new SendEmailJobListener());
    scheduler.ListenerManager.AddSchedulerListener(new SendEmailSchedulerListener());
    return scheduler;
}
