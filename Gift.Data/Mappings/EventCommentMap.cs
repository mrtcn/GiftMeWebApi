using System.Data.Entity.ModelConfiguration;
using Gift.Data.Entities;

namespace Gift.Data.Mappings {
    public class EventCommentMap : EntityTypeConfiguration<EventComment> {
        public EventCommentMap() {
            HasKey(x => x.Id);
            HasRequired(x => x.Event).WithMany(x => x.EventComments).HasForeignKey(x => x.EventId);            
        }
    }
}