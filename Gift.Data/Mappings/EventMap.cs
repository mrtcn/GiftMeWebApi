using System.Data.Entity.ModelConfiguration;
using Gift.Data.Entities;

namespace Gift.Data.Mappings {
    public class EventMap : EntityTypeConfiguration<Event> {
        public EventMap() {
            HasKey(x => x.Id);                        
        }
    }
}