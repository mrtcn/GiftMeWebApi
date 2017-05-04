using Gift.Core.BaseServices;
using Gift.Data.Entities;
using Gift.Framework.Repository;

namespace Gift.Core.Services
{
    public interface ICultureService : IBaseService<Culture> {
        
    }

    public class CultureService : BaseService<Culture>, ICultureService {

        public CultureService(IRepository<Culture> repository) : base(repository) {
        }

        protected override void OnRemoving(int id) {
            base.OnRemoved(id);
        }
    }
}
