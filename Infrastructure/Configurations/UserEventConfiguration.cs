using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ApplicationCore.Models;
using ApplicationCore.Models.Enums;

namespace Infrastructure.Configurations
{
    public class UserEventConfiguration : IEntityTypeConfiguration<UserEvent>
    {
        public void Configure(EntityTypeBuilder<UserEvent> builder)
        {            
            builder.Property(x => x.StartTime).HasConversion(x => DateTime.SpecifyKind(x, DateTimeKind.Utc), x => x);
            builder.Property(x => x.EndTime).HasConversion(x => DateTime.SpecifyKind(x, DateTimeKind.Utc), x => x);
            builder.Property(x => x.Date).HasConversion(x => DateTime.SpecifyKind(x, DateTimeKind.Utc), x => x);
            builder.Property(x => x.LastDate).HasConversion(x => DateTime.SpecifyKind(x, DateTimeKind.Utc), x => x);
            builder.Property(x => x.Recurrency).HasConversion(x => x.ToString(), x => Enum.Parse<Recurrency>(x));
            builder.HasData(new UserEvent
            {
                Id = Guid.NewGuid(),
                Name = "Test name from seed",
                Category = "test category from seed",
                Place = "test place from seed",
                StartTime = DateTime.UtcNow,
                EndTime = DateTime.UtcNow.AddHours(2),
                Date = DateTime.UtcNow,
                LastDate = DateTime.UtcNow,
                Description = "test description from seed",
                AdditionalInfo = "test additionalInfo from seed",
                ImageUrl = "test image url from seed",
                Recurrency = Recurrency.Weekly
            });
        }
    }
}
