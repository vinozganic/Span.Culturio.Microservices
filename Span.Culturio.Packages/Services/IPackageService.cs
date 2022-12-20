using System;
using Span.Culturio.Core.Models.Package;

namespace Span.Culturio.Packages.Services
{
    public interface IPackageService
    {
        Task<PackageDto> CreatePackageAsync(CreatePackageDto createPackageDto);
        Task<List<PackageDto>> GetPackagesAsync();
    }
}

