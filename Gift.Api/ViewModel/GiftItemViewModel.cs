using System;
using System.Collections.Generic;
using Gift.Core.EntityParams;
using Gift.Data.Models;

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
}