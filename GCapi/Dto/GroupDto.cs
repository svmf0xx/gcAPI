using gcapi.Models;

namespace gcapi.Dto
{
    public class GroupDto
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Emoji { get; set; } = "🌀";
        public List<UserDto>? GroupUsers { get; set; }
        public GroupStatisticDto? GroupStatistic { get; set; }

    }
}
