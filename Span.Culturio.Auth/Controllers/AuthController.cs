using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Span.Culturio.Api.Services.UserService;
using Span.Culturio.Core.Models.Auth;
using Span.Culturio.Core.Models.Users;
using Span.Culturio.Auth.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Span.Culturio.Auth.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IValidator<CreateUserDto> _validator;
        private readonly IAuthService _authService;

        public AuthController(IValidator<CreateUserDto> validator, IAuthService authService)
        {
            _validator = validator;
            _authService = authService;
        }

        /// <summary>
        /// Register new user
        /// </summary>
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> RegisterUser(CreateUserDto registerUserDto)
        {
            ValidationResult result = _validator.Validate(registerUserDto);
            if (!result.IsValid) return BadRequest("ValidationError");

            var user = await _authService.RegisterUser(registerUserDto);
            if (user is null) return BadRequest();

            return Ok("Successful response");
        }

        /// <summary>
        /// Login
        /// </summary>
        [HttpPost("login")]
        public async Task<ActionResult<TokenDto>> Login(LoginDto loginUserDto)
        {
            var token = await _authService.Login(loginUserDto);
            if (token is null) return BadRequest("Invalid username or password");

            return Ok(token);
        }
    }
}

