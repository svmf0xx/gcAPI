using gcapi.Enums;
using System.ComponentModel.DataAnnotations;

namespace gcapi.Models
{
    public class ReactionModel
    {
        [Key]
        public Guid Id { get; set; }
        public Reaction Reaction { get; set; } = Reaction.None;
        public Guid OwnerId { get; set; }
    }
}
