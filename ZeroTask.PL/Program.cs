using ZeroTask.BLL.Models;
using ZeroTask.BLL.Repositories.Contracts;
using ZeroTask.BLL.Services;
using ZeroTask.BLL.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<UserEventDataContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("Postgresql")));
builder.Services.AddScoped<IUserEventRepository, UserEventRepository>();
builder.Services.AddScoped<IUserEventService, UserEventService>();

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

PrepData(app);

app.Run();

static void PrepData(IApplicationBuilder app)
{
    using var serviceScope = app.ApplicationServices.CreateScope();
    Seed(serviceScope.ServiceProvider.GetService<UserEventDataContext>()!);
}

static void Seed(UserEventDataContext context)
{
    if (context.UserEvents.Any()) return;

    context.UserEvents.Add(new UserEvent
    {
        Name = "Test name from seed",
        Category = "test category from seed",
        Place = "test place from seed",
        Date = DateTime.UtcNow,
        Time = DateTime.UtcNow,
        Description = "test description from seed",
        AdditionalInfo = "test additionalInfo from seed",
        ImageUrl = "test image url from seed"
    });
    context.SaveChanges();
}