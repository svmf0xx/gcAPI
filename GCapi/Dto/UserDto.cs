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
        public long TgId { get; set; }
        public Roles Role { get; set; }
        public int? TimeZone { get; set; }

        public UserDto(UserModel user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            SecondName = user.SecondName;
            Email = user.Email;
            Username = user.Username;
            TgId = user.TgId;
            Role = user.Role;
            TimeZone = user.TimeZone;
        }

        
    }
}
