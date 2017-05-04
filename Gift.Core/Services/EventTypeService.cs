using Gift.Core.BaseServices;
using Gift.Data.Entities;
using Gift.Framework.Repository;

namespace Gift.Core.Services
{
    public interface IEventTypeService : IBaseService<EventType> {
        
    }

    public class EventTypeService : BaseService<EventType>, IEventTypeService {
        private readonly IRepository<EventType> _repository;

        public EventTypeService(IRepository<EventType> repository) : base(repository) {
            _repository = repository;
        }
    }
}
