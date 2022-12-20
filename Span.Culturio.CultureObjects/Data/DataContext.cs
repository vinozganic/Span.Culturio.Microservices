using Microsoft.EntityFrameworkCore;
using Span.Culturio.CultureObjects.Data.Entities;

namespace Span.Culturio.CultureObjects.Data
{
    public class DataContext : DbContext
    {
        public DbSet<CultureObject> CultureObjects { get; set; }
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