using Gift.Data.Entities;
using Gift.Data.Models;

namespace Gift.Api.ViewModel
{
    public class FriendshipStatusViewModel
    {
        public FriendshipStatus FriendshipStatus { get; set; }
    }

    public class FriendRequestViewModel
    {     
        public int? FriendId { get; set; }
        public FriendshipStatus FriendshipStatus { get; set; }
    }

    public class FriendResponseViewModel
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? FriendId { get; set; }
        public FriendshipStatus FriendshipStatus { get; set; }
    }

    public class FriendshipViewModel
    {
        public FriendshipViewModel(Friend model)
        {
            UserId = model.FriendId.GetValueOrDefault();
            UserName = model.Friendship.FirstName + ' ' + model.Friendship.LastName;
            FriendshipStatus = model.FriendshipStatus;
        }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public FriendshipStatus FriendshipStatus { get; set; }
    }
}