using ApplicationCore.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ApplicationCore.Models.Enums;

namespace Infrastructure.Configurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.Property(x => x.NotificationTime).HasConversion(x => x.ToString(), x => Enum.Parse<NotificationTime>(x));
        }
    }
}
