using Gift.Core.BaseServices;
using Gift.Data.Entities;
using Gift.Framework.Repository;

namespace Gift.Core.Services
{
    public interface IGiftAutoCompleteService : IBaseService<GiftAutoComplete> {
        
    }

    public class GiftAutoCompleteService : BaseService<GiftAutoComplete>, IGiftAutoCompleteService {
        private readonly IRepository<GiftAutoComplete> _repository;

        public GiftAutoCompleteService(IRepository<GiftAutoComplete> repository) : base(repository) {
            _repository = repository;
        }
    }
}
