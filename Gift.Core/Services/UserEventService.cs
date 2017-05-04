using Gift.Core.BaseServices;
using Gift.Data.Entities;
using Gift.Framework.Repository;

namespace Gift.Core.Services
{
    public interface IUserEventService : IBaseService<UserEvent> {
        
    }

    public class UserEventService : BaseService<UserEvent>, IUserEventService {
        private readonly IRepository<UserEvent> _repository;

        public UserEventService(IRepository<UserEvent> repository) : base(repository) {
            _repository = repository;
        }
    }
}
