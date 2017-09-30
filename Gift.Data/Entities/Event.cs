using System;
using System.Collections.Generic;
using Gift.Data.Entities.SupplementaryModels;
using Gift.Data.Models;

namespace Gift.Data.Entities {
    public interface IEvent : IEntity, IUserId, IHasStatus, ITracingFieldsModel {
        string EventName { get; set; }
        string EventImagePath { get; set; }
        string EventThumbnailPath { get; set; }
        int EventTypeId { get; set; }
        DateTime? EventDate { get; set; }
        PermissionStatus Permission { get; set; }
    }

    public class Event : TracingDateModel, IEvent
    {
        public Event()
        {
            UserEvents = new HashSet<UserEvent>();
            FavoriteEvents = new HashSet<FavoriteEvent>();
        }
        public int Id { get; set; }
        public string EventName { get; set; }
        public string EventImagePath { get; set; }
        public string EventThumbnailPath { get; set; }
        public int EventTypeId { get; set; }
        public int UserId { get; set; }
        public DateTime? EventDate { get; set; }

        public PermissionStatus Permission { get; set; }

        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public UserTypes UserType { get; set; }

        public Status Status { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<FavoriteEvent> FavoriteEvents { get; set; }
        public virtual ICollection<GiftItem> GiftItems { get; set; }
        public virtual ICollection<UserEvent> UserEvents { get; set; }
        public virtual ICollection<EventComment> EventComments { get; set; }
    }
}