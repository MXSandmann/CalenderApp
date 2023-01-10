using Microsoft.EntityFrameworkCore;
using ZeroTask.DAL.Entities;

namespace ZeroTask.DAL.DataContext
{
    public class UserEventDataContext : DbContext
    {
        public UserEventDataContext(DbContextOptions<UserEventDataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserEventDataContext).Assembly);
        }

        public DbSet<UserEvent> UserEvents { get; set; } = null!;

    }
}
