using Gift.Data.Entities.SupplementaryModels;
using Gift.Data.Models;

namespace Gift.Data.Entities {
    public interface IFavoriteEvent : IEntity, IUserId, IHasStatus, ITracingFieldsModel {
        int EventId { get; set; }
    }

    public class FavoriteEvent : TracingDateModel, IFavoriteEvent
    {
        public FavoriteEvent()
        { }
        public int Id { get; set; }
        public int EventId { get; set; }
        public int UserId { get; set; }
        
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public UserTypes UserType { get; set; }

        public Status Status { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Event Event { get; set; }
    }
}