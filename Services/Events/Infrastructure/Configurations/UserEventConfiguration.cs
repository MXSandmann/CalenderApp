using ApplicationCore.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace Infrastructure.Configurations
{
    public class UserEventConfiguration : IEntityTypeConfiguration<UserEvent>
    {
        public void Configure(EntityTypeBuilder<UserEvent> builder)
        {
            builder.Property(x => x.HasRecurrency).HasColumnType("boolean");
            builder.Property(x => x.Done).HasColumnType("boolean");
            builder.Property(x => x.StartTime).HasConversion(x => DateTime.SpecifyKind(x, DateTimeKind.Utc), x => x);
            builder.Property(x => x.EndTime).HasConversion(x => DateTime.SpecifyKind(x, DateTimeKind.Utc), x => x);
            builder.Property(x => x.Date).HasConversion(x => DateTime.SpecifyKind(x, DateTimeKind.Utc), x => x);
            builder.Property(x => x.LastDate).HasConversion(x => DateTime.SpecifyKind(x, DateTimeKind.Utc), x => x);
            builder.Property(x => x.InvitationIds).HasConversion(new ValueConverter<ICollection<string>, string>(
                v => JsonConvert.SerializeObject(v),
                v => ConvertToInvitationIds(v)));
            builder.HasOne(x => x.RecurrencyRule).WithOne(x => x.UserEvent);
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
                HasRecurrency = true,
                InvitationIds = new List<string>()
            });
        }

        private static ICollection<string> ConvertToInvitationIds(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return Array.Empty<string>();
            return JsonConvert.DeserializeObject<List<string>>(input)!;
        }
    }
}
