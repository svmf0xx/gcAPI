using gcapi.Enums;
using gcapi.Interfaces;
using gcapi.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace gcapi.DataBaseModels
{
    public class UserModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public required string FirstName { get; set; }

        [MaxLength(50)]
        public string? SecondName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
       // public string? Phone { get; set; } а зачем телефон вообще
        public long? TgId { get; set; }

        public List<GroupModel> Groups { get; set; } = [];
        public List<PlanModel> Plans { get; set; } = [];
        public List<EventModel> Events { get; set; } = [];

        //Аккаунт
        [Required]
        [MaxLength(16)]
        public required string Username { get; set; } //@vlad

        public string Secret { get; set; } //эта штука будет использоватсья для генерации одноразовых паролей через пакет OTP.NET, он тут самый популярный

        public Roles? Role { get; set; } = Roles.User;
    }
}
