using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ApplicationCore.Models;

namespace Infrastructure.Configurations
{
    public class UserEventConfiguration : IEntityTypeConfiguration<UserEvent>
    {
        public void Configure(EntityTypeBuilder<UserEvent> builder)
        {
            builder.Property(x => x.Date).HasConversion(x => DateTime.SpecifyKind(x, DateTimeKind.Utc), x => x.Date);
            builder.Property(x => x.Time).HasConversion(x => DateTime.SpecifyKind(x, DateTimeKind.Utc), x => x.ToLocalTime());
            builder.HasData(new UserEvent
            {
                Id = Guid.NewGuid(),
                Name = "Test name from seed",
                Category = "test category from seed",
                Place = "test place from seed",
                Date = DateTime.UtcNow,
                Time = DateTime.UtcNow,
                Description = "test description from seed",
                AdditionalInfo = "test additionalInfo from seed",
                ImageUrl = "test image url from seed"
            });
        }
    }
}
