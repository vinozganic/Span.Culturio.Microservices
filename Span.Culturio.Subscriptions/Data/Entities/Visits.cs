using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Span.Culturio.Subscriptions.Data.Entities
{
    public class Visits
    {
        public int Id { get; set; }
        public int? SubscriptionId { get; set; }
        public int PackageItemId { get; set; }
        public int VisitsLeft { get; set; }
    }

    public class VisitsConfiguration : IEntityTypeConfiguration<Visits>
    {
        public void Configure(EntityTypeBuilder<Visits> builder)
        {
            builder.ToTable("Visits");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.VisitsLeft).IsRequired();
        }
    }
}

