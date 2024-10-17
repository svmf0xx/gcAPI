using gcapi.Models;

namespace gcapi.Models
{
    public class InviteCodeModel
    {
        public Guid Id { get; set; }
        public UserModel Owner { get; set; }
        public GroupModel Group { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ExspiredAt { get; set; }
    }
}
