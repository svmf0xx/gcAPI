using gcapi.Enums;
using gcapi.Models;

namespace gcapi.Dto
{
    public class PlanDto
    {
        public Guid? Id { get; set; }
        public required Guid Owner { get; set; }
        public required Visible Visible { get; set; }
        public required CalObjectDto CalendarObject { get; set; }
    }
}
