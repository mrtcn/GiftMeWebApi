namespace Gift.Api.ViewModel
{
    public class GiftItemViewModel
    {
        public int Id { get; set; }
        public string GiftItemName { get; set; }
        public string GiftImagePath { get; set; }
        public int? EventId { get; set; }
        public int UserId { get; set; }
        public bool IsBought { get; set; }
    }

    public class GiftItemIdModel
    {
        public int GiftItemId { get; set; }
    }

    public class ToggleBuyStatusModel
    {
        public int Id { get; set; }
        public bool IsBought { get; set; }
    }
}