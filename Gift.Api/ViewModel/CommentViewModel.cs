namespace Gift.Api.ViewModel
{
    public class CommentIdViewModel
    {
        public int Id { get; set; }
    }

    public class GiftItemIdViewModel
    {
        public int GiftItemId { get; set; }
    }

    public class GiftItemCommentViewModel
    {
        public int Id { get; set; }
        public string CommentText { get; set; }
        public int GiftItemId { get; set; }
    }

    public class EventIdViewModel
    {
        public int EventId { get; set; }
    }

    public class EventCommentViewModel
    {
        public int Id { get; set; }
        public string CommentText { get; set; }
        public int EventId { get; set; }
    }
}