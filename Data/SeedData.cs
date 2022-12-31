using ZeroTask.Models;

namespace ZeroTask.Data
{
    public static class SeedData
    {
        public static void PrepData(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            Seed(serviceScope.ServiceProvider.GetService<DataContext>()!);
        }

        private static void Seed(DataContext context)
        {
            context.EventViewModels.Add(new EventViewModel
            {
                Name = "Test name from seed",
                Category = "test category from seed",
                Place = "test place from seed",
                Date = DateTime.Now,
                Time = DateTime.Now,
                Description = "test description from seed",
                AdditionalInfo = "test additionalInfo from seed",
                ImageUrl = "test image url from seed"
            });
            context.SaveChanges();
        }
    }
}
