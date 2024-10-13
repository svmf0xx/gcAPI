using System.ComponentModel.DataAnnotations;

namespace gcapi.Dto
{
    public class RegisterDto
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string SecondName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public long? TgId { get; set; }

        [Required]
        [MaxLength(16)]
        public string Login { get; set; }


        [Required]
        [MaxLength(50)]
        public string Password { get; set; }
    }
}
