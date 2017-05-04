using System.Data.Entity.ModelConfiguration;
using Gift.Data.Entities;

namespace Gift.Data.Mappings {
    public class GiftItemCommentMap : EntityTypeConfiguration<GiftItemComment> {
        public GiftItemCommentMap() {
            HasKey(x => x.Id);
            HasRequired(x => x.GiftItem).WithMany(x => x.GiftItemComments).HasForeignKey(x => x.GiftItemId);            
        }
    }
}