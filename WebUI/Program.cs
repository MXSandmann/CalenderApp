using ApplicationCore.Repositories.Contracts;
using ApplicationCore.Services;
using ApplicationCore.Services.Contracts;
using FluentValidation;
using Infrastructure.DataContext;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebUI.Models;
using WebUI.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<UserEventDataContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("Postgresql")));
builder.Services.AddScoped<IUserEventRepository, UserEventRepository>();
builder.Services.AddScoped<IRecurrencyRuleRepository, RecurrencyRuleRepository>();
builder.Services.AddScoped<IUserEventService, UserEventService>();
builder.Services.AddScoped<IValidator<CreateUpdateUserEventViewModel>, DateValidator>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

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