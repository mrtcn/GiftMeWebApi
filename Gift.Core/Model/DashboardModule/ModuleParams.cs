using Gift.Data.Entities.ModulePermissions;

namespace Gift.Core.Model.DashboardModule {
    public class ModuleParams : IModule, IEntityParams {
        public int Id { get; set; }
        public string ModuleName { get; set; }
    }
}