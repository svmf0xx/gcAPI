using System.ComponentModel.DataAnnotations;

namespace gcapi.Dto
{
    public class RegisterDto
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string? SecondName { get; set; }
        public string? Email { get; set; }
        public long TgId { get; set; }

        [Required]
        [MaxLength(16), MinLength(4)]
        public string Username { get; set; }
        public int? TimeZone { get; set; }
    }
}
