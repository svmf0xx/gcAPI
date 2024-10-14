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
        public string? Phone { get; set; }
        public long? TgId { get; set; }

        /// <summary>
        /// DEPRICATED!!!
        /// </summary>
        public List<Guid> UserEventsId { get; set; } = []; //а зачем

        /// <summary>
        /// DEPRICATED!!!
        /// </summary>
        public List<Guid> UserGroupsIds { get; set; } = []; //типа если указать модель, в базу и так вставится ссылка на элемент



        public List<GroupModel> Groups { get; set; } = [];

        public List<ICalendarObject> CalendarObjects { get; set; } = [];



        //Аккаунт
        [Required]
        [MaxLength(16)]
        public required string Login { get; set; }


        [Required]
        [MaxLength(50)]
        public required string PasswordHash { get; set; } //мб вообще отказаться от пароля, сделать вход только по одноразовым кодам, это +100 к крутости и безопасности

        public string? Salt { get; set; } //обычно же соль является частью пароля, а не отдельная записб
        public Roles? Role { get; set; } = Roles.User;
    }
}
