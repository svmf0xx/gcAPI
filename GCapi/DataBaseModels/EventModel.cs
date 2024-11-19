using gcapi.Models;
using gcapi.Enums;
using gcapi.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace gcapi.Models
{
    public class EventModel : ICalendarObject
    {
        [Key]
        public Guid Id { get; set; }
        public required UserModel Owner { get; set; }
        public DateTime DateTimeFrom { get; set; }
        public DateTime DateTimeTo { get; set; }
        public required string Name { get; set; }
        public string? HexColor { get; set; } = "0xFFFFFFFF";
        public string? Emoji { get; set; }

        public required GroupModel Group { get; set; }

        public string? Description { get; set; }
        
        public List<ReactionModel> Reactions { get; set; } = [];

        public required Visible Visible { get; set; }

        //public List<string>? EventUsersLogins { get; set; } = new List<string>(); DEPRICATED


        //[NotMapped]  // мне кажется я сделал супер тупо
        //да не

    }
}
