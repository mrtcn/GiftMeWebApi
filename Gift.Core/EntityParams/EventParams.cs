using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Gift.Core.Model;
using Gift.Data.Entities;
using Gift.Data.Models;

namespace Gift.Core.EntityParams {
    public class EventParams : IEntityParams, IUserId {
        public int Id { get; set; }
        public string EventImagePath { get; set; }
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
            EventImagePath = model.EventImagePath;
            UserId = model.UserId;
            EventTypeId = model.EventTypeId;
            Permission = model.Permission;
            GiftItemList = model.GiftItems?.Select(x => new GiftItemModel(x)).ToList();
            Users =
                model.UserEvents?.Select(
                    x =>
                        new AddedEventUser
                        {
                            UserId = x.UserId,
                            UserImagePath = x.User.ImagePath
                        }).ToList();
            EventOwner =
                model.UserEvents?.Select(
                    x =>
                        new AddedEventUser()
                        {
                            UserId = x.UserId,
                            UserImagePath = x.User.ImagePath
                        }).FirstOrDefault(x => x.UserId == model.UserId);
        }
        public int Id { get; set; }
        public string EventName { get; set; }
        public string EventImagePath { get; set; }
        public int EventTypeId { get; set; }
        public int UserId { get; set; }
        public DateTime? EventDate { get; set; }
        public PermissionStatus Permission { get; set; }

        public List<GiftItemModel> GiftItemList { get; set; }
        public List<AddedEventUser> Users { get; set; }
        public AddedEventUser EventOwner { get; set; }
    }

    public class EventListModel
    {
        public EventListModel()
        {

        }

        public EventListModel(Event model)
        {
            Id = model.Id;
            EventDate = model.EventDate;
            EventName = model.EventName;
            EventImagePath = model.EventImagePath;
            EventTypeId = model.EventTypeId;
            EventBoughtItemAmount = model.GiftItems.Where(x => x.IsBought && x.Status == Status.Active).Count();
            EventLeftItemAmount = model.GiftItems.Where(x => !x.IsBought && x.Status == Status.Active).Count();
            EventItemAmount = model.GiftItems.Where(x => x.Status == Status.Active).Count();
            EventOwner =
               model.UserEvents.Select(
                   x =>
                       new AddedEventUser()
                       {
                           UserId = x.UserId,
                           UserImagePath = x.User.ImagePath
                       }).FirstOrDefault(x => x.UserId == model.UserId);
        }
        public int Id { get; set; }
        public string EventName { get; set; }
        public string EventImagePath { get; set; }
        public int EventTypeId { get; set; }
        public int EventBoughtItemAmount { get; set; }
        public int EventLeftItemAmount { get; set; }
        public int EventItemAmount { get; set; }
        public DateTime? EventDate { get; set; }
        public AddedEventUser EventOwner { get; set; }
    }

    public class AddedEventUser
    {
        public int UserId { get; set; }
        public string UserImagePath { get; set; }
        public string UserName { get; set; }
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
            GiftImagePath = model.GiftImagePath;
            EventId = model.EventId;
            UserId = model.UserId;
            IsBought = model.IsBought;
        }
        public int Id { get; set; }
        public string GiftItemName { get; set; }
        public string GiftImagePath { get; set; }
        public int? EventId { get; set; }
        public int UserId { get; set; }
        public bool IsBought { get; set; }
    }
}