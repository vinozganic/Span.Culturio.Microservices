﻿using System;
namespace Span.Culturio.Core.Models.Package
{
    public class CreatePackageDto
    {
        public string Name { get; set; }
        public IEnumerable<CreatePackageItemDto> PackageItems { get; set; }
        public int ValidDays { get; set; }
    }
}

