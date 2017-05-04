using Gift.Data.Entities.SupplementaryModels;
using Gift.Data.Models;

namespace Gift.Data.Entities {
    public interface IFriend : IEntity, IHasStatus, ITracingFieldsModel {
        int? UserId { get; set; }
        int? FriendId { get; set; }
        FriendshipStatus FriendshipStatus { get; set; }
    }

    public class Friend : TracingDateModel, IFriend
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? FriendId { get; set; }
        public FriendshipStatus FriendshipStatus { get; set; }

        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public UserTypes UserType { get; set; }

        public Status Status { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual ApplicationUser Friendship { get; set; }
    }
}