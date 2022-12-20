using System;
using Span.Culturio.Core.Models.Package;

namespace Span.Culturio.Subscriptions.Services
{
    public interface IPackageService
    {
        Task<PackageDto> GetPackage(int packageId);
    }
}

