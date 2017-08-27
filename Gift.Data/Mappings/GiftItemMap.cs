using System.Data.Entity.ModelConfiguration;
using Gift.Data.Entities;

namespace Gift.Data.Mappings {
    public class GiftItemMap : EntityTypeConfiguration<GiftItem> {
        public GiftItemMap() {
            HasKey(x => x.Id);
            Property(x => x.Description).HasMaxLength(140);
            Property(x => x.Brand).HasMaxLength(60);
            Property(x => x.GiftItemName).HasMaxLength(40);
            HasOptional(x => x.Event).WithMany(x => x.GiftItems).HasForeignKey(x => x.EventId);            
        }
    }
}