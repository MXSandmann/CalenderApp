using ApplicationCore.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.Property(x => x.NotificationTime).HasConversion(x => DateTime.SpecifyKind(x, DateTimeKind.Utc), x => x);
        }
    }
}
