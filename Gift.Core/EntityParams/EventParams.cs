using System;
using System.Collections.Generic;
using System.Linq;
using Gift.Core.Model;
using Gift.Data.Entities;
using Gift.Data.Models;

namespace Gift.Core.EntityParams {
    public class EventParams : IEntityParams, IUserId {
        public int Id { get; set; }
        public string EventName { get; set; }
        public int EventTypeId { get; set; }
        public int UserId { get; set; }
        public DateTime? EventDate { get; set; }

        public PermissionStatus Permission { get; set; }

        public Status Status { get; set; }

        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public UserTypes UserType { get; set; }

        public List<GiftItemModel> GiftItemList { get; set; }
        public List<AddedEventUser> Users { get; set; }
    }

    public class EventModel
    {
        public EventModel()
        {
            
        }

        public EventModel(Event model)
        {
            Id = model.Id;
            EventDate = model.EventDate;
            EventName = model.EventName;
            UserId = model.UserId;
            EventTypeId = model.EventTypeId;
            Permission = model.Permission;
            GiftItemList = model.GiftItems.Select(x => new GiftItemModel(x)).ToList();
            Users = model.UserEvents.Select(x => new AddedEventUser {UserId = x.UserId}).ToList();
        }
        public int Id { get; set; }
        public string EventName { get; set; }
        public int EventTypeId { get; set; }
        public int UserId { get; set; }
        public DateTime? EventDate { get; set; }
        public PermissionStatus Permission { get; set; }

        public List<GiftItemModel> GiftItemList { get; set; }
        public List<AddedEventUser> Users { get; set; }
    }

    public class AddedEventUser
    {
        public int UserId { get; set; }
    }

    public class GiftItemModel
    {
        public GiftItemModel()
        {
            
        }

        public GiftItemModel(GiftItem model)
        {
            Id = model.Id;
            GiftItemName = model.GiftItemName;
            EventId = model.EventId;
            UserId = model.UserId;
            IsBought = model.IsBought;
        }
        public int Id { get; set; }
        public string GiftItemName { get; set; }
        public int? EventId { get; set; }
        public int UserId { get; set; }
        public bool IsBought { get; set; }
    }
}