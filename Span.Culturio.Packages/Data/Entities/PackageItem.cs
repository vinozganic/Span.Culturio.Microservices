using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel;

namespace Span.Culturio.Packages.Data.Entities
{
    public class PackageItem
    {
        public int Id { get; set; }
        public int CultureObjectId { get; set; }
        public int PackageId { get; set; }
        public int AvailableVisitsCount { get; set; }
    }

    public class PackageItemConfiguration : IEntityTypeConfiguration<PackageItem>
    {
        public void Configure(EntityTypeBuilder<PackageItem> builder)
        {
            builder.ToTable("PackageItems");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.AvailableVisitsCount).IsRequired();
        }
    }
}

