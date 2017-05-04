using System.Collections.Generic;
using System.Linq;
using Gift.Core.BaseServices;
using Gift.Core.EntityParams;
using Gift.Data.Entities;
using Gift.Data.Models;
using Gift.Framework.Repository;

namespace Gift.Core.Services
{
    public interface IGiftItemCommentService : IBaseService<GiftItemComment>
    {
        List<GiftItemCommentModel> GiftItemCommentsByGiftItemId(int giftItemId);
    }

    public class GiftItemCommentService : BaseService<GiftItemComment>, IGiftItemCommentService {
        private readonly IRepository<GiftItemComment> _repository;

        public GiftItemCommentService(IRepository<GiftItemComment> repository) : base(repository) {
            _repository = repository;
        }

        public List<GiftItemCommentModel> GiftItemCommentsByGiftItemId(int giftItemId)
        {
            return Entities.Where(x => x.GiftItemId == giftItemId && x.Status == Status.Active)
                .Select(x => new GiftItemCommentModel
                {
                    Id = x.Id, GiftItemId = x.GiftItemId, CommentText = x.CommentText
                }).ToList();
        }
    }
}
