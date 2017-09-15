namespace Gift.Api.ViewModel
{
    public class GiftItemViewModel
    {
        public int Id { get; set; }
        public string GiftItemName { get; set; }
        public string GiftItemImagePath { get; set; }
        public string Brand { get; set; }
        public string Link{ get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
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