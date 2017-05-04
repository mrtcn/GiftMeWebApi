using System.Data.Entity.ModelConfiguration;
using Gift.Data.Entities;

namespace Gift.Data.Mappings {
    public class GiftItemMap : EntityTypeConfiguration<GiftItem> {
        public GiftItemMap() {
            HasKey(x => x.Id);
            HasOptional(x => x.Event).WithMany(x => x.GiftItems).HasForeignKey(x => x.EventId);            
        }
    }
}