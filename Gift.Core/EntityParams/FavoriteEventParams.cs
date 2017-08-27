using Gift.Core.Model;
using Gift.Data.Models;

namespace Gift.Core.EntityParams {
    public class FavoriteEventParams : IEntityParams, IUserId {
        public FavoriteEventParams(int eventId)
        {
            EventId = eventId;
        }
        public FavoriteEventParams()
        { }
        public int Id { get; set; }
        public int EventId { get; set; }
        public int UserId { get; set; }

        public Status Status { get; set; }

        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public UserTypes UserType { get; set; }
    }

    public class FavoriteEventModel
    {
        public FavoriteEventModel()
        {
        
        }

        public FavoriteEventModel(int id, int eventId, int userId)
        {
            Id = id;
            EventId = eventId;
            UserId = userId;
        }
        public int Id { get; set; }
        public int EventId { get; set; }
        public int UserId { get; set; }
    }
}