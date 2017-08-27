using System.Data.Entity.ModelConfiguration;
using Gift.Data.Entities;

namespace Gift.Data.Mappings {
    public class EventMap : EntityTypeConfiguration<Event> {
        public EventMap() {
            HasKey(x => x.Id);
            Property(x => x.EventName).HasMaxLength(40);
            HasRequired(x => x.User).WithMany().HasForeignKey(x => x.UserId)
            .WillCascadeOnDelete(false);
            HasMany(x => x.FavoriteEvents).WithRequired(x => x.Event)
                .HasForeignKey(x => x.EventId)
                .WillCascadeOnDelete(false);
        }
    }
}