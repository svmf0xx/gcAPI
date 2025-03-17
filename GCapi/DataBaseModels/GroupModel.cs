using gcapi.Models;

namespace gcapi.Models
{
    public class GroupModel
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string Emoji { get; set; } = "🌀";
        public long? TgId { get; set; }
        public List<UserModel> GroupUsers { get; set; } = [];
        public List<EventModel> GroupEvents { get; set; } = [];
    }
}
