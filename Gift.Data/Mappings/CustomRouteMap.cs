using System.Data.Entity.ModelConfiguration;
using Gift.Data.Entities.SupplementaryModels;

namespace Gift.Data.Mappings {
    public class CustomRouteMap : EntityTypeConfiguration<CustomRoute> {
        public CustomRouteMap() {
            HasKey(x => x.Id);
            Property(x => x.Url).HasMaxLength(255);
            Property(x => x.SeoTitle).HasMaxLength(255);
            Property(x => x.MetaKeyword).HasMaxLength(500);
            Property(x => x.MetaDescription).HasMaxLength(500); 
        }
    }
}