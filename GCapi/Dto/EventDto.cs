using gcapi.Enums;
using gcapi.Models;

namespace gcapi.Dto
{
    public class EventDto
    {
        public Guid? Id { get; set; }
        public List<ReactionModel> Reactions { get; set; } = [];
        public Guid GroupId { get; set; }
        public string? GroupName { get; set; }
        public CalObjectDto CalendarObject { get; set; }

        public EventDto(EventModel ev)
        {
            Id = ev.Id;
            Reactions = ev.Reactions;
            GroupId = ev.Group.Id;
            GroupName = ev.Group.Name;
            CalendarObject = new CalObjectDto()
            {
                Owner = ev.Owner.Id,
                DateTimeFrom = ev.DateTimeFrom,
                DateTimeTo = ev.DateTimeTo,
                Name = ev.Name,
                Visible = ev.Visible,
                Emoji = ev.Emoji,
                HexColor = ev.HexColor,
                Description = ev.Description
            };
        }

        public EventDto() //без этого всё сломается
        {                 //ну логично, это как сказать что есть влад которому надо дать инсулин, но нет просто влада
        }
    }
}
