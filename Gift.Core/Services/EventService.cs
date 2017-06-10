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
        List<EventModel> EventList(EventListType eventListType, int userId);
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

        public List<EventModel> EventList(EventListType eventListType, int userId)
        {
            switch (eventListType)
            {
                case EventListType.OwnEvents:
                    return UserEventList(userId);
                case EventListType.PublicEvents:
                    return PublicEventList();
                case EventListType.FriendsEvents:
                    return FriendsEventList(userId);
                default:
                    return null;
            }
        }

        private List<EventModel> UserEventList(int userId)
        {
            return Entities.Include(x => x.UserEvents).Include(x => x.UserEvents.Select(y => y.User)).Include(x => x.GiftItems)
                .Where(x => x.UserId == userId && x.Status == Status.Active)
                .Select(x => new EventModel
                {
                    EventOwner = new AddedEventUser
                    {                        
                        UserId = x.User.Id,
                        FullName = x.User.FullName,
                        UserImagePath = "http://192.168.0.32:54635/areas/dashboard" + x.User.ImagePath
                    },
                    Users =
                        x.UserEvents.Where(y => y.EventId == x.Id && y.Status == Status.Active)
                            .Select(z => new AddedEventUser
                            {
                                UserId = z.User.Id,
                                FullName = z.User.FullName,
                                UserImagePath = "http://192.168.0.32:54635/areas/dashboard" + z.User.ImagePath
                            })
                            .ToList(),
                    EventDate = x.EventDate,
                    EventName = x.EventName,
                    EventImagePath = "http://192.168.0.32:54635/areas/dashboard" + x.EventImagePath,
                    UserId = userId,
                    EventTypeId = x.EventTypeId,
                    GiftItemList = x.GiftItems
                        .Select(y => new GiftItemModel
                        {
                            Id = y.Id,
                            EventId = y.EventId,
                            GiftItemName = y.GiftItemName,
                            GiftImagePath = "http://192.168.0.32:54635/areas/dashboard" + y.GiftImagePath,
                            IsBought = y.IsBought,
                            UserId = y.UserId
                        }).ToList(),
                    Id = x.Id,
                    Permission = x.Permission
                }).ToList();
        }

        private List<EventModel> PublicEventList()
        {
            return Entities.Include(x => x.UserEvents).Include(x => x.UserEvents.Select(y => y.User)).Include(x => x.GiftItems).Take(100)
                .Where(x => x.Permission == PermissionStatus.Everyone && x.Status == Status.Active)
                .Select(x => new EventModel
                {
                    EventOwner = new AddedEventUser
                    {
                        UserId = x.User.Id,
                        FullName = x.User.FullName,
                        UserImagePath = "http://192.168.0.32:54635/areas/dashboard" + x.User.ImagePath
                    },
                    Users =
                        x.UserEvents.Where(y => y.EventId == x.Id && y.Status == Status.Active)
                            .Select(z => new AddedEventUser
                            {
                                UserId = z.User.Id,
                                FullName = z.User.FullName,
                                UserImagePath = "http://192.168.0.32:54635/areas/dashboard" + z.User.ImagePath
                            })
                            .ToList(),
                    EventDate = x.EventDate,
                    EventName = x.EventName,
                    EventImagePath = "http://192.168.0.32:54635/areas/dashboard" + x.EventImagePath,
                    UserId = x.UserId,
                    EventTypeId = x.EventTypeId,
                    GiftItemList = x.GiftItems
                        .Select(y => new GiftItemModel
                        {
                            Id = y.Id,
                            EventId = y.EventId,
                            GiftItemName = y.GiftItemName,
                            GiftImagePath = "http://192.168.0.32:54635/areas/dashboard" + y.GiftImagePath,
                            IsBought = y.IsBought,
                            UserId = y.UserId
                        }).ToList(),
                    Id = x.Id,
                    Permission = x.Permission
                }).OrderByDescending(x => x.EventDate).ToList();
        }

        private List<EventModel> FriendsEventList(int userId)
        {
            var friendIds = _friendService.GetUserFriendIds(userId);

            return Entities.Include(x => x.UserEvents).Include(x => x.UserEvents.Select(y => y.User)).Include(x => x.GiftItems).Take(100)
                .Where(x => x.Status == Status.Active && friendIds.Contains(x.UserId))
                .Select(x => new EventModel
                {
                    EventOwner = new AddedEventUser
                    {
                        UserId = x.User.Id,
                        FullName = x.User.FullName,
                        UserImagePath = "http://192.168.0.32:54635/areas/dashboard" + x.User.ImagePath
                    },
                    Users =
                        x.UserEvents.Where(y => y.EventId == x.Id && y.Status == Status.Active)
                            .Select(z => new AddedEventUser
                            {
                                UserId = z.User.Id,
                                FullName = z.User.FullName,
                                UserImagePath = "http://192.168.0.32:54635/areas/dashboard" + z.User.ImagePath
                            })
                            .ToList(),
                    EventDate = x.EventDate,
                    EventName = x.EventName,
                    EventImagePath = "http://192.168.0.32:54635/areas/dashboard" + x.EventImagePath,
                    UserId = x.UserId,
                    EventTypeId = x.EventTypeId,
                    GiftItemList = x.GiftItems
                        .Select(y => new GiftItemModel
                        {
                            Id = y.Id,
                            EventId = y.EventId,
                            GiftItemName = y.GiftItemName,
                            GiftImagePath = "http://192.168.0.32:54635/areas/dashboard" + y.GiftImagePath,
                            IsBought = y.IsBought,
                            UserId = y.UserId
                        }).ToList(),
                    Id = x.Id,
                    Permission = x.Permission
                }).OrderByDescending(x => x.EventDate).ToList();
        }
    }
}