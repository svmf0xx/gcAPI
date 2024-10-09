using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace gcapi.Models
{
    public class EventModel
    {
        public Guid Id { get; set; }

        public Guid GroupId { get; set; }
        public GroupModel Group { get; set; }

        public string EventHeader { get; set; }
        public string? EventDescription { get; set; } = String.Empty;

        public List<string>? EventUsersLogins { get; set; } = new List<string>();


        [NotMapped] public List<ReactionModel>? Reactions { get; set; } = new List<ReactionModel>();
        [NotMapped] public ThemeData ThemeData { get; set; } = new ThemeData(); // мне кажется я сделал супер тупо
    }
}
