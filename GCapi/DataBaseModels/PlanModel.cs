using gcapi.Enums;
using gcapi.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace gcapi.Models
{
    /// <summary>
    /// Это типа личные планы пользователя
    /// </summary>
    public class PlanModel : ICalendarObject
    {
        [Key]
        public Guid Id { get; set; }
        public required UserModel Owner { get; set; }
        public DateTime DateTimeFrom { get; set; }
        public DateTime DateTimeTo { get; set; }
        public required string Name { get; set; }

        public string? Description { get; set; }
        
        public required Visible Visible { get; set; }
        public string HexColor { get; set; } = "0xFFFFFFFF";
        public string Emoji { get; set; }
    }
}
