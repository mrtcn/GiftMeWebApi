using Gift.Core.Model;
using Gift.Data.Models;

namespace Gift.Core.EntityParams {
    public class GiftItemParams : IEntityParams, IUserId {
        public int Id { get; set; }
        public string GiftItemName { get; set; }
        public string GiftItemImagePath { get; set; }
        public string GiftItemThumbnailPath { get; set; }
        public string Brand { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public int? EventId { get; set; }
        public int UserId { get; set; }
        public bool IsBought { get; set; }

        public Status Status { get; set; }

        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public UserTypes UserType { get; set; }
    }
}