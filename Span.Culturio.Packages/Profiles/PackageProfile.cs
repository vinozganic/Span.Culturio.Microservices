using System;
using AutoMapper;
using Span.Culturio.Core.Models.Package;
using Span.Culturio.Packages.Data.Entities;

namespace Span.Culturio.Packages.Profiles
{
    public class PackageProfile : Profile
    {

        public PackageProfile()
        {
            CreateMap<CreatePackageDto, Package>();

            CreateMap<CreatePackageItemDto, PackageItem>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CultureObjectId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.AvailableVisitsCount, opt => opt.MapFrom(src => src.AvailableVisits));

            CreateMap<Package, PackageDto>().ForMember(dest => dest.CultureObjects, opt => opt.Ignore());

            CreateMap<PackageItem, PackageItemDto>()
                .ForMember(dest => dest.AvailableVisits, opt => opt.MapFrom(src => src.AvailableVisitsCount))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CultureObjectId));
        }
    }
}

