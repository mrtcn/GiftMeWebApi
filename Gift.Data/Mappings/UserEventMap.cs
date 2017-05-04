using System.Data.Entity.ModelConfiguration;
using Gift.Data.Entities;

namespace Gift.Data.Mappings {
    public class UserEventMap : EntityTypeConfiguration<UserEvent> {
        public UserEventMap() {
            HasKey(x => x.Id);
            HasRequired(x => x.Event).WithMany(x => x.UserEvents).HasForeignKey(x => x.EventId);
            HasRequired(x => x.User).WithMany(x => x.UserEvents).HasForeignKey(x => x.UserId);
        }
    }
}