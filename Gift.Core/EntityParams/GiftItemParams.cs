using Gift.Core.Model;
using Gift.Data.Models;

namespace Gift.Core.EntityParams {
    public class GiftItemParams : IEntityParams, IUserId {
        public int Id { get; set; }
        public string GiftItemName { get; set; }
        public string GiftImagePath { get; set; }
        public int? EventId { get; set; }
        public int UserId { get; set; }
        public bool IsBought { get; set; }

        public Status Status { get; set; }

        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public UserTypes UserType { get; set; }
    }
}