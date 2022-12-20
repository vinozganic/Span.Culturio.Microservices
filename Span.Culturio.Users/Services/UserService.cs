using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Span.Culturio.Auth.Helpers;
using Span.Culturio.Core.Models.Users;
using Span.Culturio.Users.Data;

namespace Span.Culturio.Users.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public UserService(DataContext dataContext, IMapper mapper, IConfiguration config)
        {
            _dataContext = dataContext;
            _mapper = mapper;
            _config = config;
        }


        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _dataContext.Users.FindAsync(id);
            if (user is null) return null;

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UsersDto> GetUsersAsync(int pageSize, int pageIndex)
        {
            var users = await _dataContext.Users
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            var data = _mapper.Map<IEnumerable<UserDto>>(users);

            var usersDto = new UsersDto
            {
                Data = data,
                TotalCount = await _dataContext.Users.CountAsync()
            };

            return usersDto;
        }

    }
}

