using System.Collections.Generic;
using System.Linq;
using Gift.Core.BaseServices;
using Gift.Data.Entities;
using Gift.Data.Models;
using Gift.Framework.Repository;

namespace Gift.Core.Services
{
    public interface IFriendService : IBaseService<Friend>
    {
        int GetFriendshipId(int userId, int friendId);
        List<Friend> GetUserFriends(int userId, FriendshipStatus friendshipStatus);
        List<int> GetUserFriendIds(int userId);
    }

    public class FriendService : BaseService<Friend>, IFriendService
    {
        private readonly IRepository<Friend> _repository;

        public FriendService(IRepository<Friend> repository) : base(repository)
        {
            _repository = repository;
        }

        public int GetFriendshipId(int userId, int friendId)
        {
            var friendship = Entities.
                FirstOrDefault(x =>
                    x.UserId == userId
                    && x.FriendId == friendId
                );

            return friendship?.Id ?? 0;
        }

        public List<int> GetUserFriendIds(int userId)
        {
            return Entities
                .Where(x => x.UserId == userId && x.Status == Status.Active)
                .Select(x => x.FriendId).ToList()
                .Select(x => x.GetValueOrDefault()).ToList();
        }

        public List<Friend> GetUserFriends(int userId, FriendshipStatus friendshipStatus)
        {
            return Entities.Where(x => x.UserId == userId
                                       && x.FriendshipStatus == friendshipStatus
                                       && x.Status == Status.Active
            ).ToList();
        }
    }
}