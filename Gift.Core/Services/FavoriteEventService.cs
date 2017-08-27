using System.Collections.Generic;
using System.Linq;
using Gift.Core.BaseServices;
using Gift.Core.EntityParams;
using Gift.Data.Entities;
using Gift.Framework.Repository;

namespace Gift.Core.Services
{
    public interface IFavoriteEventService : IBaseService<FavoriteEvent>
    {
        List<FavoriteEventModel> FavoriteEventList(int userId);
        FavoriteEventModel GetFavoriteEvent(int userId, int eventId);
    }

    public class FavoriteEventService : BaseService<FavoriteEvent>, IFavoriteEventService {
        private readonly IRepository<FavoriteEvent> _repository;

        public FavoriteEventService(IRepository<FavoriteEvent> repository) : base(repository) {
            _repository = repository;
        }

        public List<FavoriteEventModel> FavoriteEventList(int userId) {
            return Entities.Where(x => x.Status == Data.Models.Status.Active && x.UserId == userId)
                .Select(x => new FavoriteEventModel { Id = x.Id, EventId = x.EventId, UserId = x.UserId }).ToList();
        }

        public FavoriteEventModel GetFavoriteEvent(int userId, int eventId)
        {
            return Entities.Where(x => x.Status == Data.Models.Status.Active)
                .Select(x => new FavoriteEventModel { Id = x.Id, EventId = x.EventId, UserId = x.UserId})
                .FirstOrDefault(x => x.UserId == userId && x.EventId == eventId);
        }
    }
}
