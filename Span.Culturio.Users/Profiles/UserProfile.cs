using System;
using AutoMapper;
using Span.Culturio.Core.Models.Users;
using Span.Culturio.Users.Data.Entities;

namespace Span.Culturio.Users.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}

