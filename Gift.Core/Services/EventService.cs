﻿using System.Collections.Generic;
using System.Linq;
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
                    return OwnEventsList(userId);
                case EventListType.PublicEvents:
                    return PublicEventsList(userId);
                case EventListType.FriendsEvents:
                    return FriendsEventsList(userId);
                default:
                    return null;
            }
        }

        private List<EventModel> OwnEventsList(int userId)
        {
            return Entities
                .Where(x => x.UserId == userId && x.Status == Status.Active)
                .Select(x => new EventModel
                {
                    Users =
                        x.UserEvents.Where(y => y.User.Id == userId)
                            .Select(z => new AddedEventUser {UserId = z.User.Id})
                            .ToList(),
                    EventDate = x.EventDate,
                    EventName = x.EventName,
                    UserId = userId,
                    EventTypeId = x.EventTypeId,
                    GiftItemList = x.GiftItems
                        .Select(y => new GiftItemModel(y)).ToList(),
                    Id = x.Id,
                    Permission = x.Permission
                }).ToList();
        }

        private List<EventModel> PublicEventsList(int userId)
        {
            return Entities.Take(100)
                .Where(x => x.Permission == PermissionStatus.Everyone && x.Status == Status.Active)
                .Select(x => new EventModel
                {
                    Users =
                        x.UserEvents.Where(y => y.User.Id == x.UserId)
                            .Select(z => new AddedEventUser { UserId = z.User.Id })
                            .ToList(),
                    EventDate = x.EventDate,
                    EventName = x.EventName,
                    UserId = x.UserId,
                    EventTypeId = x.EventTypeId,
                    GiftItemList = x.GiftItems
                        .Select(y => new GiftItemModel(y)).ToList(),
                    Id = x.Id,
                    Permission = x.Permission
                }).OrderByDescending(x => x.EventDate).ToList();
        }

        private List<EventModel> FriendsEventsList(int userId)
        {
            var friendIds = _friendService.GetUserFriendIds(userId);

            return Entities.Take(100)
                .Where(x => x.Status == Status.Active && friendIds.Contains(x.UserId))
                .Select(x => new EventModel
                {
                    Users =
                        x.UserEvents.Where(y => y.User.Id == x.UserId)
                            .Select(z => new AddedEventUser { UserId = z.User.Id })
                            .ToList(),
                    EventDate = x.EventDate,
                    EventName = x.EventName,
                    UserId = x.UserId,
                    EventTypeId = x.EventTypeId,
                    GiftItemList = x.GiftItems
                        .Select(y => new GiftItemModel(y)).ToList(),
                    Id = x.Id,
                    Permission = x.Permission
                }).OrderByDescending(x => x.EventDate).ToList();
        }
    }
}