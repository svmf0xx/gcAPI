using gcapi.Enums;
using gcapi.Models;

namespace gcapi.Dto
{
    public class EventDto
    {
        public Guid Id { get; set; }
        public List<ReactionDto> Reactions { get; set; } = [];
        public Guid GroupId { get; set; }
        public string? GroupName { get; set; }
        public CalObjectDto CalendarObject { get; set; }
        public EventDto(EventModel ev)
        {
            Id = ev.Id;
            GroupId = ev.Group.Id;
            GroupName = ev.Group.Name;
            CalendarObject = new CalObjectDto()
            {
                Owner = ev.Owner.Id,
                OwnerData = new OwnerDto
                {
                    FirstName = ev.Owner.FirstName,
                    SecondName = ev.Owner.SecondName,
                    Username = ev.Owner.Username
                },
                DateTimeFrom = ev.DateTimeFrom,
                DateTimeTo = ev.DateTimeTo,
                Name = ev.Name,
                Visible = ev.Visible,
                Emoji = ev.Emoji,
                HexColor = ev.HexColor,
                Description = ev.Description
            };
        }

        public EventDto()
        {
        }
    }
}
