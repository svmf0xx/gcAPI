using System.ComponentModel.DataAnnotations;

namespace gcapi.Dto
{
    public class UpdateUserDto
    {
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? TelegramId { get; set; }
        public List<Guid> UserGroupsIds { get; set; } = new List<Guid>();
    }
}
