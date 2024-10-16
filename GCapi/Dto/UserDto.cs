using gcapi.Enums;
using gcapi.Models;
using System.ComponentModel.DataAnnotations;

namespace gcapi.Dto
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? Email { get; set; }
        public string Username { get; set; }
        public Roles Role { get; set; } = Roles.User;

    }
}
