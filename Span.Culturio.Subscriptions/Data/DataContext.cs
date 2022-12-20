using System;
using Microsoft.EntityFrameworkCore;
using Span.Culturio.Subscriptions.Data.Entities;

namespace Span.Culturio.Subscriptions.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Visits> Visits { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
        }
    }
}

