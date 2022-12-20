using System;
using AutoMapper;
using Span.Culturio.Core.Models.Subscription;
using Span.Culturio.Subscriptions.Data.Entities;

namespace Span.Culturio.Subscriptions.Profiles
{
    public class SubscriptionProfile : Profile
    {

        public SubscriptionProfile()
        {
            CreateMap<CreateSubscriptionDto, Subscription>()
                .ForMember(dest => dest.Id, otp => otp.Ignore())
                .ForMember(dest => dest.ActiveFrom, otp => otp.Ignore())
                .ForMember(dest => dest.ActiveTo, otp => otp.Ignore())
                .ForMember(dest => dest.State, otp => otp.Ignore())
                .ForMember(dest => dest.RecordedVisits, otp => otp.Ignore());
            CreateMap<Subscription, SubscriptionDto>();
        }
    }
}

