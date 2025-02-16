using gcapi.Models;
using gcapi.Enums;
using System.ComponentModel.DataAnnotations;

namespace gcapi.Interfaces
{
    public interface ICalendarObject
    {
        [Key]
        public Guid Id { get; set; }
        public UserModel Owner { get; set; }

        public DateTime DateTimeFrom { get; set; }

        public DateTime DateTimeTo { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        //public IEventStyle Style { get; set; } 

        public string HexColor { get; set; }


        public string? Emoji { get; set; }

        public Visible Visible { get; set; }
        
    }
}
