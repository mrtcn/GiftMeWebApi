using Gift.Core.BaseServices;
using Gift.Data.Entities;
using Gift.Framework.Repository;

namespace Gift.Core.Services
{
    public interface IGiftItemService : IBaseService<GiftItem> {
        
    }

    public class GiftItemService : BaseService<GiftItem>, IGiftItemService {
        private readonly IRepository<GiftItem> _repository;

        public GiftItemService(IRepository<GiftItem> repository) : base(repository) {
            _repository = repository;
        }
    }
}
