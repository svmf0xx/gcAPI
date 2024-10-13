using gcapi.DataBaseModels;

namespace gcapi.Models
{
    public class GroupModel
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public List<UserModel> GroupUsers { get; set; } = [];
        public List<EventModel> GroupEvents { get; set; } = [];
    }
}
