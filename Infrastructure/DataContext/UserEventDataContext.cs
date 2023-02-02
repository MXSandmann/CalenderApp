﻿using ApplicationCore.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataContext
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
        public DbSet<RecurrencyRule> RecurrencyRules { get; set; } = null!;

    }
}
