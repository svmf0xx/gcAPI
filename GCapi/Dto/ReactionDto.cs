using gcapi.Enums;
using gcapi.Models;
using System.Runtime.CompilerServices;

namespace gcapi.Dto
{
    public class ReactionDto
    {
        public Reaction Reaction { get; set; } = Reaction.None;
        public string? OwnerFirstName { get; set; }
        public string? OwnerSecondName { get; set; }
        public string? OwnerUsername { get; set; }
        public string? OwnerEmoji { get; set; }
    }
}
