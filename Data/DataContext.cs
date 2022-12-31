using Microsoft.EntityFrameworkCore;
using ZeroTask.Models;

namespace ZeroTask.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<EventViewModel> EventViewModels { get; set; }

    }
}
