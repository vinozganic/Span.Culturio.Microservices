using Span.Culturio.Core.Models.Users;
using Span.Culturio.Core.Models.Auth;
using Span.Culturio.Api.Services.UserService;
using AutoMapper;
using Span.Culturio.Auth.Data;
using Microsoft.EntityFrameworkCore;
using Span.Culturio.Auth.Helpers;
using Span.Culturio.Auth.Data.Entities;

namespace Span.Culturio.Auth.Services
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public AuthService(DataContext dataContext, IMapper mapper, IConfiguration config)
        {
            _dataContext = dataContext;
            _mapper = mapper;
            _config = config;
        }

        public async Task<UserDto> RegisterUser(CreateUserDto registerUserDto)
        {
            var user = _mapper.Map<User>(registerUserDto);

            if (user is null) return null;

            AuthHelper.CreatePasswordHash(registerUserDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _dataContext.Add(user);
            await _dataContext.SaveChangesAsync();

            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public async Task<TokenDto> Login(LoginDto loginUserDto)
        {
            var user = await _dataContext.Users.SingleOrDefaultAsync(x => x.Username == loginUserDto.Username);

            if (user is null) return null;

            if (!AuthHelper.VerifyPasswordHash(loginUserDto.Password, user.PasswordHash, user.PasswordSalt)) return null;

            var token = AuthHelper.CreateToken(loginUserDto, _config.GetSection("JWT_KEY").Value);

            return new TokenDto { Token = token };
        }
    }
}

