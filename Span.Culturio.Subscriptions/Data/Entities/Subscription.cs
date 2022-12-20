using System;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Span.Culturio.Subscriptions.Data.Entities
{
    public class Subscription
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PackageId { get; set; }
        public string Name { get; set; }
        public DateTime? ActiveFrom { get; set; }
        public DateTime? ActiveTo { get; set; }
        public string State { get; set; }
        public int RecordedVisits { get; set; }
    }

    public class SubscriptionsConfiguration : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.ToTable("Subscriptions");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.ActiveFrom).HasDefaultValue(null);
            builder.Property(x => x.ActiveTo).HasDefaultValue(null);
            builder.Property(x => x.State).HasDefaultValue("inactive");
            builder.Property(x => x.RecordedVisits).HasDefaultValue(0);
        }
    }
}

