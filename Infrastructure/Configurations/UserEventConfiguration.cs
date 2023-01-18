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
            builder.Property(x => x.StartDateTime).HasConversion(x => DateTime.SpecifyKind(x, DateTimeKind.Utc), x => x.ToLocalTime());
            builder.Property(x => x.EndDateTime).HasConversion(x => DateTime.SpecifyKind(x, DateTimeKind.Utc), x => x.ToLocalTime());
            builder.Property(x => x.Recurrency).HasConversion(x => x.ToString(), x => Enum.Parse<Recurrency>(x));
            builder.HasData(new UserEvent
            {
                Id = Guid.NewGuid(),
                Name = "Test name from seed",
                Category = "test category from seed",
                Place = "test place from seed",
                StartDateTime = DateTime.UtcNow,
                EndDateTime = DateTime.UtcNow.AddHours(2),
                Description = "test description from seed",
                AdditionalInfo = "test additionalInfo from seed",
                ImageUrl = "test image url from seed",
                Recurrency = Recurrency.Weekly
            });
        }
    }
}
