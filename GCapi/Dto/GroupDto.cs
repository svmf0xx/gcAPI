using gcapi.Models;

namespace gcapi.Dto
{
    public class GroupDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Emoji { get; set; } = "🌀";
        public List<string> GroupUsers { get; set; } //наверное сюда лучше подавать юзернеймы

        //public List<EventModel> GroupEvents { get; set; } по идее это никогда не будет меняться непосредственно вместе с группой
    }
}
