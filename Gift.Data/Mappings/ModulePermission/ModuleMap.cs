using System.Data.Entity.ModelConfiguration;
using Gift.Data.Entities.ModulePermissions;

namespace Gift.Data.Mappings.ModulePermission {
    public class ModuleMap : EntityTypeConfiguration<Module> {
        public ModuleMap() {
            HasKey(x => x.Id);
            Property(x => x.ModuleName).HasMaxLength(255);            
        }
    }
}