using Gift.Core.Model;
using Gift.Data.Entities;
using Gift.Data.Models;

namespace Gift.Core.EntityParams {
    public class GiftItemCommentParams : IEntityParams, IUserId {
        public int Id { get; set; }
        public string CommentText { get; set; }
        public int GiftItemId { get; set; }
        public int UserId { get; set; }

        public Status Status { get; set; }

        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public UserTypes UserType { get; set; }
    }

    public class GiftItemCommentModel
    {
        public GiftItemCommentModel(){ }

        public GiftItemCommentModel(GiftItemComment model)
        {
            Id = model.Id;
            GiftItemId = model.GiftItemId;
            CommentText = model.CommentText;
        }
        public int Id { get; set; }
        public string CommentText { get; set; }
        public int GiftItemId { get; set; }
    }
}