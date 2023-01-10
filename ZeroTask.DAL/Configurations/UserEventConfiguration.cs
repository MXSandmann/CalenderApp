using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZeroTask.DAL.Entities;

namespace ZeroTask.DAL.Configurations
{
    public class UserEventConfiguration : IEntityTypeConfiguration<UserEvent>
    {
        public void Configure(EntityTypeBuilder<UserEvent> builder)
        {
            builder.Property(x => x.Date).HasConversion(x => DateTime.SpecifyKind(x, DateTimeKind.Utc), x => x.Date);
            builder.Property(x => x.Time).HasConversion(x => DateTime.SpecifyKind(x, DateTimeKind.Utc), x => x.ToLocalTime());
        }
    }
}
