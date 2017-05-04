using System.Data.Entity.ModelConfiguration;
using Gift.Data.Entities;

namespace Gift.Data.Mappings {
    public class CountryMap : EntityTypeConfiguration<Country> {
        public CountryMap() {
            HasKey(x => x.Id);
            Property(x => x.Name).HasMaxLength(255);
        }
    }
}