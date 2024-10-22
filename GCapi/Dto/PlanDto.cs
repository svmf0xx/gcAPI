using gcapi.Enums;
using gcapi.Models;

namespace gcapi.Dto
{
    public class PlanDto
    {
        public Guid? Id { get; set; }

        // надеюсь я не нарушаю твой гениальный замысел
        public required CalObjectDto CalendarObject { get; set; }
    }
}
