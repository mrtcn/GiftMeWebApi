using Gift.Core.Model;
using Gift.Data.Entities;
using Gift.Data.Models;

namespace Gift.Core.EntityParams {
    public class FriendParams : IFriend, IEntityParams {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? FriendId { get; set; }
        public FriendshipStatus FriendshipStatus { get; set; }

        public Status Status { get; set; }

        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public UserTypes UserType { get; set; }
    }
}