using gcapi.Enums;
using System.ComponentModel.DataAnnotations;

namespace gcapi.Models
{
    public class UserModel
    {
        [Key]
        public Guid Id { get; set; }
        public string Emoji { get; set; } = "😘";

        [Required]
        [MaxLength(50)]
        public required string FirstName { get; set; }

        [MaxLength(50)]
        public string? SecondName { get; set; }
        public string? Email { get; set; }
        public required long TgId { get; set; }
        public int? TimeZone { get; set; } = 0;
        public List<GroupModel> Groups { get; set; } = [];
        public List<PlanModel> Plans { get; set; } = [];
        public List<EventModel> Events { get; set; } = [];

        //Аккаунт
        [Required]
        [MaxLength(16), MinLength(4)]
        public required string Username { get; set; }

        public string? Secret { get; set; }

        public Roles Role { get; set; } = Roles.User;
    }
}
