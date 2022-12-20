using System;
using AutoMapper;
using Span.Culturio.Core.Models.Users;
using Span.Culturio.Auth.Data.Entities;

namespace Span.Culturio.Users.Profiles
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<CreateUserDto, User>();
        }
    }
}

