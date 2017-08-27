using System.Data.Entity.ModelConfiguration;
using Gift.Data.Entities;

namespace Gift.Data.Mappings {
    public class FavoriteEventMap : EntityTypeConfiguration<FavoriteEvent> {
        public FavoriteEventMap() {
            HasKey(x => x.Id);
        }
    }
}