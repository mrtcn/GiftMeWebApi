using System;
using System.Collections.Generic;
using Gift.Core.EntityParams;
using Gift.Data.Models;

namespace Gift.Api.ViewModel
{
    public class EventViewModel
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public string EventImagePath { get; set; }
        public int EventTypeId { get; set; }
        public int EventOwnerId { get; set; }
        public DateTime? EventDate { get; set; }
        public PermissionStatus Permission { get; set; }

        public List<GiftItemModel> GiftItemList { get; set; }
        public List<AddedEventUser> Users { get; set; }
    }

    public class EventListModel
    {
        public EventListType EventListType { get; set; }
    }
}