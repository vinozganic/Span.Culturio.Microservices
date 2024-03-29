﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Span.Culturio.Packages.Data.Entities
{
    public class Package
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ValidDays { get; set; }

    }

    public class PackageConfiguration : IEntityTypeConfiguration<Package>
    {
        public void Configure(EntityTypeBuilder<Package> builder)
        {
            builder.ToTable("Packages");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        }
    }
}

