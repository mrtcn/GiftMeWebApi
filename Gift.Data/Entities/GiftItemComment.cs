using Gift.Data.Entities.SupplementaryModels;
using Gift.Data.Models;

namespace Gift.Data.Entities {
    public interface IGiftItemComment : IEntity, IUserId, IHasStatus, ITracingFieldsModel {
        string CommentText { get; set; }
        int GiftItemId { get; set; }
    }

    public class GiftItemComment : TracingDateModel, IGiftItemComment
    {
        public int Id { get; set; }
        public string CommentText { get; set; }
        public int GiftItemId { get; set; }
        public int UserId { get; set; }

        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public UserTypes UserType { get; set; }

        public Status Status { get; set; }
        public virtual GiftItem GiftItem { get; set; }
    }
}