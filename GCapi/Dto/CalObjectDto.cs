using gcapi.Enums;
using gcapi.Models;

namespace gcapi.Dto
{
    public class CalObjectDto
    {
        public required Guid Owner { get; set; }
        public required DateTime DateTimeFrom { get; set; }
        public required DateTime DateTimeTo { get; set; }
        public required string Name { get; set; }
        public EventColor Color { get; set; }
        public string? Emoji { get; set; }

        public string? Description { get; set; }

    }
}
