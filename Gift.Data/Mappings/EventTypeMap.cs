using System.Data.Entity.ModelConfiguration;
using Gift.Data.Entities;

namespace Gift.Data.Mappings {
    public class EventTypeMap : EntityTypeConfiguration<EventType> {
        public EventTypeMap() {
            HasKey(x => x.Id);
        }
    }
}