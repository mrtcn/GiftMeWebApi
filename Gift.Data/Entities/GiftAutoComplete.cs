using Gift.Data.Entities.SupplementaryModels;
using Gift.Data.Models;

namespace Gift.Data.Entities {
    public interface IGiftAutoComplete : IEntity, IHasStatus, ITracingFieldsModel {
        string GiftAutoCompleteName { get; set; }
    }

    public class GiftAutoComplete : TracingDateModel, IGiftAutoComplete
    {
        public int Id { get; set; }
        public string GiftAutoCompleteName { get; set; }

        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public UserTypes UserType { get; set; }

        public Status Status { get; set; }
    }
}