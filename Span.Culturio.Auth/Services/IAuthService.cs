using System;
using Span.Culturio.Core.Models.Auth;
using Span.Culturio.Core.Models.Users;

namespace Span.Culturio.Api.Services.UserService
{
    public interface IAuthService
    {
        Task<UserDto> RegisterUser(CreateUserDto registerUserDto);
        Task<TokenDto> Login(LoginDto loginUserDto);
    }
}

