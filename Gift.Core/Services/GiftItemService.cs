using System.Collections.Generic;
using System.Linq;
using Gift.Core.BaseServices;
using Gift.Core.EntityParams;
using Gift.Data.Entities;
using Gift.Framework.Repository;

namespace Gift.Core.Services
{
    public interface IGiftItemService : IBaseService<GiftItem>
    {
        List<GiftItemModel> GiftItemListByEventId(int eventId, int userId);
        int GetGiftItemStatus(bool isBought, int giftOwnerId, int eventOwnerId, List<int> eventAttendeeIds, int userId);
    }

    public class GiftItemService : BaseService<GiftItem>, IGiftItemService {
        private readonly IRepository<GiftItem> _repository;

        public GiftItemService(IRepository<GiftItem> repository) : base(repository) {
            _repository = repository;
        }

        public List<GiftItemModel> GiftItemListByEventId(int eventId, int userId)
        {
            
                var gift = Entities.Select(x =>
                new GiftItemModel
                {
                    Id = x.Id,
                    EventId = x.EventId,
                    EventOwnerId = x.Event.UserId,
                    EventAttendeeIds = x.Event.UserEvents.Where(z => z.Status == Data.Models.Status.Active).Select(z => z.UserId).ToList(),
                    UserId = x.UserId,
                    GiftItemImagePath = CoreSettings.BaseUrl + x.GiftItemImagePath,
                    Brand = x.Brand,
                    Amount = x.Amount,
                    Description = x.Description,
                    IsBought = x.IsBought,
                    GiftItemName = x.GiftItemName
                }).Where(x => x.EventId == eventId).ToList();
            gift.ForEach(x => x.GiftItemStatus = GetGiftItemStatus(x.IsBought, x.UserId, x.EventOwnerId, x.EventAttendeeIds, userId));
            return gift;
        }

        public int GetGiftItemStatus(bool isBought, int giftOwnerId, int eventOwnerId, List<int> eventAttendeeIds, int userId)
        {
            if (!isBought && eventAttendeeIds.Contains(userId) )
            {
                // Free To Reserve
                return 0;
            }
            else if (isBought && (userId == giftOwnerId || userId == eventOwnerId))
            {
                // Reserved and has right to cancel it
                return 1;
            }
            else
            {
                // Reserved and doesn't have right to cancel it
                return 2;
            }
        }
    }    
}
