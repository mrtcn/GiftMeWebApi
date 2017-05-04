using Gift.Core.BaseServices;
using Gift.Data.Entities;
using Gift.Framework.Repository;

namespace Gift.Core.Services
{
    public interface IDistrictService : IBaseService<District> {
        
    }

    public class DistrictService : BaseService<District>, IDistrictService {
        private readonly IRepository<District> _repository;

        public DistrictService(IRepository<District> repository) : base(repository) {
            _repository = repository;
        }
    }
}
