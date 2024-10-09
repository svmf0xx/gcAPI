namespace gcapi.Models
{
    public class GroupModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<string> GroupUsersLogins { get; set; }
        public List<EventModel> GroupEvents { get; set; }
    }
}
