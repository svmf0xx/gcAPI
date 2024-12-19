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
       // public string? Phone { get; set; } а зачем телефон вообще
        public required long TgId { get; set; }
        public int? TimeZone { get; set; } = 0;
        public List<GroupModel> Groups { get; set; } = [];
        public List<PlanModel> Plans { get; set; } = [];
        public List<EventModel> Events { get; set; } = [];

        //Аккаунт
        [Required]
        [MaxLength(16), MinLength(4)]
        public required string Username { get; set; } //@vlad

        public string? Secret { get; set; } //эта штука будет использоватсья для генерации одноразовых паролей через пакет OTP.NET, он тут самый популярный

        public Roles Role { get; set; } = Roles.User;
    }
}
