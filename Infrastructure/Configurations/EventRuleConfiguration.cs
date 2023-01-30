using ApplicationCore.Models.Entities;
using ApplicationCore.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class EventRuleConfiguration : IEntityTypeConfiguration<RecurrencyRule>
    {
        public void Configure(EntityTypeBuilder<RecurrencyRule> builder)
        {
            builder.Property(x => x.Recurrency).HasConversion(x => x.ToString(), x => Enum.Parse<Recurrency>(x));            
            builder.Property(x => x.WeekOfMonth).HasConversion(x => x.ToString(), x => Enum.Parse<WeekOfTheMonth>(x));
            builder.Property(x => x.MonthOfYear).HasConversion(x => x.ToString(), x => Enum.Parse<MonthOfTheYear>(x));
            builder.HasOne(x => x.UserEvent).WithOne(x => x.RecurrencyRule);
        }
    }
}
