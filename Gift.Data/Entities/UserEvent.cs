using Gift.Data.Entities.SupplementaryModels;
using Gift.Data.Models;

namespace Gift.Data.Entities
{
    public interface IUserEvent : IEntity, IHasStatus, ITracingFieldsModel
    {
        int UserId { get; set; }
        int EventId { get; set; }
    }

    public class UserEvent : TracingDateModel, IUserEvent
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }

        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public UserTypes UserType { get; set; }

        public Status Status { get; set; }
        public virtual Event Event { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
