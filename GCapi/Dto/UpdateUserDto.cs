﻿using System.ComponentModel.DataAnnotations;

namespace gcapi.Dto
{
    public class UpdateUserDto
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string? SecondName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public long TgId { get; set; }
    }
}
