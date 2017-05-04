using Gift.Core.BaseServices;
using Gift.Data.Entities;
using Gift.Framework.Repository;

namespace Gift.Core.Services
{
    public interface ICountryService : IBaseService<Country> {
        
    }

    public class CountryService : BaseService<Country>, ICountryService {
        private readonly IRepository<Country> _repository;

        public CountryService(IRepository<Country> repository) : base(repository) {
            _repository = repository;
        }
    }
}
