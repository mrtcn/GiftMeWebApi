using System.Data.Entity.ModelConfiguration;
using Gift.Data.Entities;

namespace Gift.Data.Mappings {
    public class FriendMap : EntityTypeConfiguration<Friend> {
        public FriendMap() {
            HasKey(x => x.Id);
            //HasRequired(x => x.User).WithMany(x => x.Friends).HasForeignKey(x => x.UserId);
            //HasRequired(x => x.Friendship).WithMany(x => x.Friends).HasForeignKey(x => x.FriendId);
        }
    }
}