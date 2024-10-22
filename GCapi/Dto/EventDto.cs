using gcapi.Enums;
using gcapi.Models;

namespace gcapi.Dto
{
    public class EventDto
    {
        public Guid? Id { get; set; }
        public required Guid Owner { get; set; }
        public List<ReactionModel> Reactions { get; set; } = [];
        public required Guid GroupId { get; set; }
        public required CalObjectDto CalendarObject { get; set; }
    }
}
