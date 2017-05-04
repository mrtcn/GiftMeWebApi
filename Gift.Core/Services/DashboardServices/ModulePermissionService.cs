using Gift.Core.BaseServices;
using Gift.Data.Entities.ModulePermissions;
using Gift.Framework.Repository;

namespace Gift.Core.Services.DashboardServices
{
    public interface IModulePermissionService : IBaseService<ModulePermission> {
        
    }

    public class ModulePermissionService : BaseService<ModulePermission>, IModulePermissionService
    {
        private readonly IRepository<ModulePermission> _repository;

        public ModulePermissionService(IRepository<ModulePermission> repository) : base(repository) {
            _repository = repository;
        }
    }
}
