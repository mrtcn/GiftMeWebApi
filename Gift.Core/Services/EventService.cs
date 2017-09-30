using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using AutoMapper;
using Gift.Core.BaseServices;
using Gift.Core.EntityParams;
using Gift.Core.Model;
using Gift.Data.Entities;
using Gift.Data.Models;
using Gift.Framework.Repository;

namespace Gift.Core.Services
{
    public interface IEventService : IBaseService<Event>
    {
        List<EventListModel> EventList(EventListType eventListType, int userId, string searchTerm = null);
        EventListModel GetEventById(int id, int userId);
    }

    public class EventService : BaseService<Event>, IEventService
    {
        private readonly IGiftItemService _giftItemService;
        private readonly IRepository<Event> _repository;
        private readonly IUserEventService _userEventService;
        private readonly IFriendService _friendService;

        public EventService(
            IRepository<Event> repository,
            IUserEventService userEventService,
            IGiftItemService giftItemService, 
            IFriendService friendService) : base(repository)
        {
            _repository = repository;
            _userEventService = userEventService;
            _giftItemService = giftItemService;
            _friendService = friendService;
        }

        protected override void OnSaveChanged(Event entity, IEntityParams entityParams)
        {
            OnSaveChanging(entity, entityParams);

            var eventParams = entityParams as EventParams;

            //Create User Event Association Table
            if (eventParams?.Users != null && eventParams.Users.Any())
                foreach (var eventParamsUser in eventParams.Users)
                {
                    var userEventParams = new UserEventParams
                    {
                        UserId = eventParamsUser.UserId,
                        EventId = eventParams.Id
                    };
                    _userEventService.CreateOrUpdate(userEventParams);
                }

            // Create Added Gifts
            if (eventParams?.GiftItemList != null && eventParams.GiftItemList.Any())
                foreach (var giftItemModel in eventParams.GiftItemList)
                {
                    giftItemModel.EventId = entity.Id;

                    var giftItemParams = new GiftItemParams();
                    Mapper.Map(giftItemModel, giftItemParams);

                    _giftItemService.CreateOrUpdate(giftItemParams);
                }
        }

        public EventListModel GetEventById(int id, int userId)
        {
            return Entities                
                .Select(x => new EventListModel
                {
                    EventOwner = new AddedEventUser
                    {
                        UserId = x.User.Id,
                        UserImagePath = CoreSettings.BaseUrl + x.User.ImagePath,
                        UserThumbnailPath = CoreSettings.BaseUrl + x.User.ThumbnailPath,
                        UserName = x.User.FirstName + " " + x.User.LastName
                    },
                    EventDate = x.EventDate,
                    EventName = x.EventName,
                    EventBoughtItemAmount = x.GiftItems.Where(z => z.IsBought && z.Status == Status.Active).Count(),
                    EventLeftItemAmount = x.GiftItems.Where(z => !z.IsBought && z.Status == Status.Active).Count(),
                    EventItemAmount = x.GiftItems.Where(z => z.Status == Status.Active).Count(),
                    EventImagePath = CoreSettings.BaseUrl + x.EventImagePath,
                    EventTypeId = x.EventTypeId,
                    IsFavoriteEvent = x.FavoriteEvents.Any(z => z.EventId == id && z.UserId == userId && z.Status == Status.Active),
                    Id = x.Id
                }).FirstOrDefault(x => x.Id == id);
        }

        public List<EventListModel> EventList(EventListType eventListType, int userId, string searchTerm = null)
        {
            switch (eventListType)
            {
                case EventListType.OwnEvents:
                    return UserEventList(userId, searchTerm);
                case EventListType.PublicEvents:
                    return PublicEventList(searchTerm);
                case EventListType.FriendsEvents:
                    return FriendsEventList(userId, searchTerm);
                default:
                    return null;
            }
        }

        private List<EventListModel> UserEventList(int userId, string searchTerm = null)
        {
            return Entities.Where(x => x.UserId == userId && x.Status == Status.Active && (searchTerm == null || x.EventName.Contains(searchTerm)))
                .Select(x => new EventListModel
                {
                    EventOwner = new AddedEventUser
                    {
                        UserId = x.User.Id,
                        UserImagePath = CoreSettings.BaseUrl  + x.User.ImagePath,
                        UserThumbnailPath = CoreSettings.BaseUrl + x.User.ThumbnailPath,
                        UserName = x.User.FirstName + " " + x.User.LastName
                    },
                    EventDate = x.EventDate,
                    EventName = x.EventName,
                    EventBoughtItemAmount = x.GiftItems.Where(z => z.IsBought && z.Status == Status.Active).Count(),
                    EventLeftItemAmount = x.GiftItems.Where(z => !z.IsBought && z.Status == Status.Active).Count(),
                    EventItemAmount = x.GiftItems.Where(z => z.Status == Status.Active).Count(),
                    EventImagePath = CoreSettings.BaseUrl  + x.EventImagePath,
                    EventTypeId = x.EventTypeId,
                    Id = x.Id
                }).ToList();
        }

        private List<EventListModel> PublicEventList(string searchTerm = null)
        {
            return Entities.Take(100)
                .Where(x => x.Permission == PermissionStatus.Everyone && x.Status == Status.Active && (searchTerm == null || x.EventName.Contains(searchTerm)))
                .Select(x => new EventListModel
                {
                    EventOwner = new AddedEventUser
                    {
                        UserId = x.User.Id,
                        UserImagePath = CoreSettings.BaseUrl  + x.User.ImagePath,
                        UserThumbnailPath = CoreSettings.BaseUrl + x.User.ThumbnailPath,
                        UserName = x.User.FirstName + " " + x.User.LastName
                    },
                    EventDate = x.EventDate,
                    EventName = x.EventName,
                    EventBoughtItemAmount = x.GiftItems.Where(z => z.IsBought && z.Status == Status.Active).Count(),
                    EventLeftItemAmount = x.GiftItems.Where(z => !z.IsBought && z.Status == Status.Active).Count(),
                    EventItemAmount = x.GiftItems.Where(z => z.Status == Status.Active).Count(),
                    EventImagePath = CoreSettings.BaseUrl  + x.EventImagePath,                   
                    EventTypeId = x.EventTypeId,                    
                    Id = x.Id
                }).OrderByDescending(x => x.EventDate).ToList();
        }

        private List<EventListModel> FriendsEventList(int userId, string searchTerm = null)
        {
            var friendIds = _friendService.GetUserFriendIds(userId);

            return Entities.Take(100)
                .Where(x => x.Status == Status.Active && friendIds.Contains(x.UserId) && (searchTerm == null || x.EventName.Contains(searchTerm)))
                .Select(x => new EventListModel
                {
                    EventOwner = new AddedEventUser
                    {
                        UserId = x.User.Id,
                        UserImagePath = CoreSettings.BaseUrl  + x.User.ImagePath,
                        UserThumbnailPath = CoreSettings.BaseUrl + x.User.ThumbnailPath,
                        UserName = x.User.FirstName + " " + x.User.LastName
                    },
                    EventDate = x.EventDate,
                    EventName = x.EventName,
                    EventBoughtItemAmount = x.GiftItems.Where(z => z.IsBought && z.Status == Status.Active).Count(),
                    EventLeftItemAmount = x.GiftItems.Where(z => !z.IsBought && z.Status == Status.Active).Count(),
                    EventItemAmount = x.GiftItems.Where(z => z.Status == Status.Active).Count(),
                    EventImagePath = CoreSettings.BaseUrl  + x.EventImagePath,                 
                    EventTypeId = x.EventTypeId,                    
                    Id = x.Id
                }).OrderByDescending(x => x.EventDate).ToList();
        }
    }
}