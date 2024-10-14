using gcapi.DataBaseModels;
using gcapi.Enums;

namespace gcapi.Interfaces
{
    public interface ICalendarObject
    {
        public Guid Id { get; set; }
        public UserModel Owner { get; set; }

        public DateTime DateTimeFrom { get; set; }

        public DateTime DateTimeTo { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public IEventStyle Style { get; set; }

        public Visible Visible { get; set; }

    }
}
