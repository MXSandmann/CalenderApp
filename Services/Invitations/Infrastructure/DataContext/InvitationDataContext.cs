using ApplicationCore.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataContext
{
    public class InvitationDataContext : DbContext
    {
        public InvitationDataContext(DbContextOptions<InvitationDataContext> options) : base(options)
        {

        }

        public DbSet<Invitation> Invitations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(InvitationDataContext).Assembly);
        }
    }
}
