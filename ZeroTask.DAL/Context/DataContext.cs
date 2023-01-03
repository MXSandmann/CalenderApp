using Microsoft.EntityFrameworkCore;
using ZeroTask.DAL.Entities;

namespace ZeroTask.DAL.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<UserEvent> UserEvents { get; set; }

    }
}
