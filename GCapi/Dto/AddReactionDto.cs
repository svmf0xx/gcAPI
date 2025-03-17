using gcapi.Enums;

namespace gcapi.Dto
{
    public class AddReactionDto
    {
        public Guid EventId { get; set; }
        public Reaction Reaction { get; set; }
        public Guid OwnerId { get; set; }
    }
}
