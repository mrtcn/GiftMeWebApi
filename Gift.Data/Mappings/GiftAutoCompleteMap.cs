using System.Data.Entity.ModelConfiguration;
using Gift.Data.Entities;

namespace Gift.Data.Mappings {
    public class GiftAutoCompleteMap : EntityTypeConfiguration<GiftAutoComplete> {
        public GiftAutoCompleteMap() {
            HasKey(x => x.Id);
        }
    }
}