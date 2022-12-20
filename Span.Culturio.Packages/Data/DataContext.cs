using System;
using Microsoft.EntityFrameworkCore;
using Span.Culturio.Packages.Data.Entities;
using System.ComponentModel;

namespace Span.Culturio.Packages.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Package> Packages { get; set; }
        public DbSet<PackageItem> PackageItems { get; set; }

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

