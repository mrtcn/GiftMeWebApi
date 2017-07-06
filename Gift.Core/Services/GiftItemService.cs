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
        List<GiftItemModel> GiftItemListByEventId(int eventId);
    }

    public class GiftItemService : BaseService<GiftItem>, IGiftItemService {
        private readonly IRepository<GiftItem> _repository;

        public GiftItemService(IRepository<GiftItem> repository) : base(repository) {
            _repository = repository;
        }

        public List<GiftItemModel> GiftItemListByEventId(int eventId)
        {
            return
                Entities.Select(
                    x =>
                        new GiftItemModel
                        {
                            Id = x.Id,
                            EventId = x.EventId,
                            UserId = x.UserId,
                            GiftImagePath = x.GiftImagePath,
                            IsBought = x.IsBought,
                            GiftItemName = x.GiftItemName
                        }).Where(x => x.EventId == eventId).ToList();
        }
    }
}
