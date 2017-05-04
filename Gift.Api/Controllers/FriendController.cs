using System.Linq;
using System.Web.Http;
using Gift.Api.ViewModel;
using Gift.Core.EntityParams;
using Gift.Core.Services;
using Microsoft.AspNet.Identity;

namespace Gift.Api.Controllers
{
    [RoutePrefix("api/Friend"), Authorize]
    public class FriendController : ApiController
    {
        private readonly IFriendService _friendService;

        public FriendController(
            IFriendService eventService)
        {
            _friendService = eventService;
        }

        //It will send or answer friendship request upon FriendshipStatus
        [Route("SendFriendshipRequest")]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer), HttpPost]
        public IHttpActionResult SendFriendshipRequest(FriendRequestViewModel model)
        {
            var userId = User.Identity.GetUserId<int>();

            //If exists, get Friend Id by userId and friendId otherwise Id is null
            var friendshipId = _friendService.GetFriendshipId(userId, model.FriendId.GetValueOrDefault());

            var friendParams = new FriendParams();
            AutoMapper.Mapper.Map(model, friendParams);

            friendParams.Id = friendshipId;
            _friendService.CreateOrUpdate(friendParams);
            return Ok();
        }

        //It will send or answer friendship request upon FriendshipStatus
        [Route("FriendshipList")]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer), HttpPost]
        public IHttpActionResult FriendshipList(FriendshipStatusViewModel model)
        {
            var userId = User.Identity.GetUserId<int>();
            var friendshipList = _friendService.GetUserFriends(userId, model.FriendshipStatus).Select(x => new FriendshipViewModel(x));

            return Ok(friendshipList);
        }
    }
}