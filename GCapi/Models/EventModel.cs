using System.Net;

namespace gcapi.Models
{
    public class EventModel
    {
        public int Id { get; set; }
        public string EventHeader { get; set; }
        public string EventDescription { get; set; } = String.Empty;
        public List<UserModel> EventUsers { get; set; } = new List<UserModel>();

    }
}
