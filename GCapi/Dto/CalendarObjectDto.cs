using gcapi.Enums;
using gcapi.Models;

namespace gcapi.Dto
{
    public class CalendarObjectDto
    {
        public Guid? Id { get; set; }
        public required Guid Owner { get; set; }
        public required DateTime DateTimeFrom { get; set; }
        public required DateTime DateTimeTo { get; set; }
        public required string Name { get; set; }
        public EventColor Color { get; set; }
        public string Emoji { get; set; }

        public required GroupModel Group { get; set; }

        public string? Description { get; set; }

        public List<ReactionModel> Reactions { get; set; } = [];

        public required Visible Visible { get; set; }
    }
}
