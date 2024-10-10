using gcapi.Enums;

namespace gcapi.Models
{
    public class ReactionModel
    {
        public Reaction Reaction { get; set; } = Reaction.None;
        public Guid OwnerId { get; set; }
    }
}
