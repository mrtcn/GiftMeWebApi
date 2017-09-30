using System;
using System.Collections.Generic;
using System.Linq;
using Gift.Core.Model;
using Gift.Data.Entities;
using Gift.Data.Models;

namespace Gift.Core.EntityParams {
    public class EventParams : IEntityParams, IUserId {
        public int Id { get; set; }
        public string EventImagePath { get; set; }
        public string EventThumbnailPath { get; set; }
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
            EventThumbnailPath = model.EventThumbnailPath;
            UserId = model.UserId;
            EventTypeId = model.EventTypeId;
            Permission = model.Permission;            
            IsFavoriteEvent = model.FavoriteEvents
                .Any(x => x.EventId == model.Id && x.UserId == model.UserId && x.Status == Status.Active);           
            EventOwner =
                model.UserEvents?.Select(
                    x =>
                        new AddedEventUser()
                        {
                            UserId = x.UserId,
                            UserImagePath = x.User.ImagePath,
                            UserThumbnailPath = x.User.ThumbnailPath
                        }).FirstOrDefault(x => x.UserId == model.UserId);
        }
        public int Id { get; set; }
        public string EventName { get; set; }
        public string EventImagePath { get; set; }
        public string EventThumbnailPath { get; set; }
        public bool IsFavoriteEvent { get; set; }
        public int EventTypeId { get; set; }
        public int UserId { get; set; }
        public DateTime? EventDate { get; set; }
        public PermissionStatus Permission { get; set; }

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
            EventThumbnailPath = model.EventThumbnailPath;
            EventTypeId = model.EventTypeId;
            EventBoughtItemAmount = model.GiftItems.Where(x => x.IsBought && x.Status == Status.Active).Count();
            EventLeftItemAmount = model.GiftItems.Where(x => !x.IsBought && x.Status == Status.Active).Count();
            EventItemAmount = model.GiftItems.Where(x => x.Status == Status.Active).Count();
            IsFavoriteEvent = model.FavoriteEvents.Any(x => x.EventId == model.Id && x.UserId == model.UserId && x.Status == Status.Active);
            EventOwner =
               model.UserEvents.Select(
                   x =>
                       new AddedEventUser()
                       {
                           UserId = x.UserId,
                           UserImagePath = x.User.ImagePath,
                           UserThumbnailPath = x.User.ThumbnailPath
                       }).FirstOrDefault(x => x.UserId == model.UserId);
        }
        public int Id { get; set; }
        public string EventName { get; set; }
        public string EventImagePath { get; set; }
        public string EventThumbnailPath { get; set; }
        public int EventTypeId { get; set; }
        public int EventBoughtItemAmount { get; set; }
        public int EventLeftItemAmount { get; set; }
        public int EventItemAmount { get; set; }        
        public bool IsFavoriteEvent { get; set; }
        public DateTime? EventDate { get; set; }
        public AddedEventUser EventOwner { get; set; }
    }

    public class AddedEventUser
    {
        public int UserId { get; set; }
        public string UserImagePath { get; set; }
        public string UserThumbnailPath { get; set; }
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
            GiftItemImagePath = model.GiftItemImagePath;
            GiftItemThumbnailPath = model.GiftItemThumbnailPath;
            Brand = model.Brand;
            Description = model.Description;
            Amount = model.Amount;
            EventId = model.EventId;
            EventOwnerId = model.Event.UserId;
            EventAttendeeIds = model.Event.UserEvents.Select(x => x.UserId).ToList();
            UserId = model.UserId;
            IsBought = model.IsBought;
        }
        
        public int Id { get; set; }
        public string GiftItemName { get; set; }
        public string GiftItemImagePath { get; set; }
        public string GiftItemThumbnailPath { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public int? EventId { get; set; }
        public int EventOwnerId { get; set; }
        public List<int> EventAttendeeIds { get; set; }
        public int UserId { get; set; }
        public bool IsBought { get; set; }
        public int GiftItemStatus { get; set; }
    }
}