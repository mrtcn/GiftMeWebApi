using System.Collections.Generic;
using System.Linq;
using Gift.Core.BaseServices;
using Gift.Core.EntityParams;
using Gift.Data.Entities;
using Gift.Data.Models;
using Gift.Framework.Repository;

namespace Gift.Core.Services
{
    public interface IEventCommentService : IBaseService<EventComment>
    {
        List<EventCommentModel> GiftItemCommentsByGiftItemId(int eventId);
    }

    public class EventCommentService : BaseService<EventComment>, IEventCommentService {
        private readonly IRepository<EventComment> _repository;

        public EventCommentService(IRepository<EventComment> repository) : base(repository) {
            _repository = repository;
        }

        public List<EventCommentModel> GiftItemCommentsByGiftItemId(int eventId)
        {
            return Entities.Where(x => x.EventId == eventId && x.Status == Status.Active)
                .Select(x => new EventCommentModel
                {
                    Id = x.Id,
                    EventId = x.EventId,
                    CommentText = x.CommentText
                }).ToList();
        }
    }
}
