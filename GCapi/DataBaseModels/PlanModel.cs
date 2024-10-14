using gcapi.DataBaseModels;
using gcapi.Enums;
using gcapi.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace gcapi.Models
{
    /// <summary>
    /// Это типа личные планы пользователя
    /// </summary>
    public class PlanModel : ICalendarObject
    {
        [Key]
        public Guid Id { get; set; }
        public CalendarObjectType Type { get; set; } = CalendarObjectType.Plan; //ладно это не будет работать так как я хотел
        public required UserModel Owner { get; set; }
        public DateTime DateTimeFrom { get; set; }
        public DateTime DateTimeTo { get; set; }
        public required string Name { get; set; }

        public string? Description { get; set; }
        
        public required Visible Visible { get; set; }
        public EventColor Color { get; set; }
        public string Emoji { get; set; }
    }
}
