using Gift.Data.Entities.SupplementaryModels;
using Gift.Data.Models;

namespace Gift.Data.Entities {
    public interface IEventType : IEntity, IHasStatus, ITracingFieldsModel {
        string EventTypeName { get; set; }
    }

    public class EventType : TracingDateModel, IEventType
    {
        public int Id { get; set; }
        public string EventTypeName { get; set; }

        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public UserTypes UserType { get; set; }

        public Status Status { get; set; }
    }
}