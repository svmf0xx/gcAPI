using System.Net;

namespace gcapi.Models
{
    public class EventModel
    {
        public Guid Id { get; set; }
        public string EventHeader { get; set; }
        public string? EventDescription { get; set; } = String.Empty;
        public List<Guid>? EventUsersId { get; set; } = new List<Guid>();
    }
}
