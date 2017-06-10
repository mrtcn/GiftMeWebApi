using System.Data.Entity.ModelConfiguration;
using Gift.Data.Entities;

namespace Gift.Data.Mappings {
    public class EventMap : EntityTypeConfiguration<Event> {
        public EventMap() {
            HasKey(x => x.Id);
            HasRequired(x => x.User).WithMany().HasForeignKey(x => x.UserId)
            .WillCascadeOnDelete(false);
        }
    }
}