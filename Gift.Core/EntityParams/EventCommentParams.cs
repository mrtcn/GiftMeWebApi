using Gift.Core.Model;
using Gift.Data.Entities;
using Gift.Data.Models;

namespace Gift.Core.EntityParams {
    public class EventCommentParams : IEntityParams, IUserId {
        public int Id { get; set; }
        public string CommentText { get; set; }
        public int EventId { get; set; }
        public int UserId { get; set; }

        public Status Status { get; set; }

        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public UserTypes UserType { get; set; }
    }

    public class EventCommentModel
    {
        public EventCommentModel() { }

        public EventCommentModel(EventComment model)
        {
            Id = model.Id;
            EventId = model.EventId;
            CommentText = model.CommentText;
        }
        public int Id { get; set; }
        public string CommentText { get; set; }
        public int EventId { get; set; }
    }
}