using Gift.Core.BaseServices;
using Gift.Framework.Repository;
using Gift.Data.Entities.ModulePermissions;

namespace Gift.Core.Services.DashboardServices
{
    public interface IModuleService : IBaseService<Module> {
        
    }

    public class ModuleService : BaseService<Module>, IModuleService
    {
        private readonly IRepository<Module> _repository;

        public ModuleService(IRepository<Module> repository) : base(repository) {
            _repository = repository;
        }
    }

    //public interface IModuleCulturedService : ICulturedBaseService<Module>
    //{

    //}

    //public class ModuleCulturedService : CulturedBaseService<Module>, IModuleService
    //{
    //    private readonly IRepository<Module> _repository;

    //    public ModuleCulturedService(IRepository<Module> repository) : base(repository)
    //    {
    //        _repository = repository;
    //    }
    //}
}
