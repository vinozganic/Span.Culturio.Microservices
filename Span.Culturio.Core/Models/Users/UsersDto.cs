using System;
namespace Span.Culturio.Core.Models.Users
{
    public class UsersDto
    {
        public IEnumerable<UserDto> Data { get; set; }
        public int TotalCount { get; set; }
    }
}

