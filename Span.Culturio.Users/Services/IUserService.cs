using System;
using Span.Culturio.Core.Models.Users;

namespace Span.Culturio.Users.Services
{
    public interface IUserService
    {
        Task<UsersDto> GetUsersAsync(int pageSize, int pageIndex);
        Task<UserDto> GetUserByIdAsync(int id);
        //Task<TokenDto> Login(LoginDto loginUserdto);
    }
}

