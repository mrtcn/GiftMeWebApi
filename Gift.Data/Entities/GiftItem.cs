using System.Collections.Generic;
using Gift.Data.Entities.SupplementaryModels;
using Gift.Data.Models;

namespace Gift.Data.Entities {
    public interface IGiftItem : IEntity, IUserId, IHasStatus, ITracingFieldsModel {
        string GiftItemName { get; set; }
        string GiftImagePath { get; set; }
        string Brand { get; set; }
        string Description { get; set; }
        int Amount { get; set; }
        int? EventId { get; set; }
        bool IsBought { get; set; }
    }

    public class GiftItem : TracingDateModel, IGiftItem
    {
        public int Id { get; set; }
        public string GiftItemName { get; set; }
        public string GiftImagePath { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public int? EventId { get; set; }
        public int UserId { get; set; }
        public bool IsBought { get; set; }

        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public UserTypes UserType { get; set; }

        public Status Status { get; set; }
        public virtual Event Event { get; set; }
        public virtual ICollection<GiftItemComment> GiftItemComments { get; set; }
    }
}