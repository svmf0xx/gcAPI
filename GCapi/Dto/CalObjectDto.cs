using gcapi.Enums;
using gcapi.Models;

namespace gcapi.Dto
{
    public class CalObjectDto
    {
        public Guid Owner { get; set; }
        public OwnerDto? OwnerData { get; set; }
        public DateTime DateTimeFrom { get; set; }
        public DateTime DateTimeTo { get; set; }
        public string Name { get; set; }
        public string HexColor { get; set; } = "0xFFFFFFFF";
        public Visible Visible { get; set; } // потому что видимость есть в бд у всех (типа лучше оставить, потому что можно придумать фичи для ивентов с ней хз) 
        public string? Emoji { get; set; }
        public string? Description { get; set; }
    }
}
