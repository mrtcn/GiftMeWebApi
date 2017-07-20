using System.Web.Http;
using Gift.Api.Models;
using Gift.Api.ViewModel;
using Gift.Core.EntityParams;
using Gift.Core.Services;
using Microsoft.AspNet.Identity;

namespace Gift.Api.Controllers
{
    [RoutePrefix("api/Comment"), Authorize]
    public class CommentController : BaseController
    {
        private readonly IGiftItemCommentService _giftItemCommentService;
        private readonly IEventCommentService _eventCommentService;

        public CommentController(
            IGiftItemCommentService giftItemCommentService, 
            IEventCommentService eventCommentService)
        {
            _giftItemCommentService = giftItemCommentService;
            _eventCommentService = eventCommentService;
        }

        [Route("GiftItemCommentList")]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer), HttpPost]
        public IHttpActionResult GiftItemCommentList(GiftItemIdViewModel model)
        {
            var giftItemComments = _giftItemCommentService.GiftItemCommentsByGiftItemId(model.GiftItemId);
            return SuccessResponse(new SuccessModel(giftItemComments));
        }

        [Route("AddOrUpdateGiftItemComment")]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer), HttpPost]
        public IHttpActionResult AddOrUpdateGiftItemComment(GiftItemCommentViewModel model)
        {
            var giftItemCommentParams = new GiftItemCommentParams();
            AutoMapper.Mapper.Map(model, giftItemCommentParams);

            /* Get and Assign UserId */
            giftItemCommentParams.UserId = User.Identity.GetUserId<int>();

            //Check If User is updating his/her own comment
            var isUsersOwnEvent = _giftItemCommentService.CheckUserProperty(giftItemCommentParams);

            if (!isUsersOwnEvent)
                return ErrorResponse(new ErrorModel(null, Resources.WebApiResource.SavingCommentFailed, 1));

            var giftItemComment = _giftItemCommentService.CreateOrUpdate(giftItemCommentParams);
            return SuccessResponse(new SuccessModel(new GiftItemCommentModel(giftItemComment)));
        }

        [Route("RemoveGiftItemComment")]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer), HttpPost]
        public IHttpActionResult RemoveGiftItemComment(CommentIdViewModel model)
        {
            _giftItemCommentService.Remove(model.Id);
            return SuccessResponse(new SuccessModel(null));
        }

        [Route("EventCommentList")]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer), HttpPost]
        public IHttpActionResult EventCommentList(EventIdViewModel model)
        {
            var eventComments = _eventCommentService.GiftItemCommentsByGiftItemId(model.EventId);
            return SuccessResponse(new SuccessModel(eventComments));
        }

        [Route("AddOrUpdateEventComment")]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer), HttpPost]
        public IHttpActionResult AddOrUpdateEventComment(EventCommentViewModel model)
        {
            var eventCommentParams = new EventCommentParams();
            AutoMapper.Mapper.Map(model, eventCommentParams);

            /* Get and Assign UserId */
            eventCommentParams.UserId = User.Identity.GetUserId<int>();

            //Check If User is updating his/her own comment
            var isUsersOwnEvent = _eventCommentService.CheckUserProperty(eventCommentParams);

            if (!isUsersOwnEvent)
                return ErrorResponse(new ErrorModel(null, Resources.WebApiResource.SavingCommentFailed, 1));

            var eventComment = _eventCommentService.CreateOrUpdate(eventCommentParams);
            return SuccessResponse(new SuccessModel(new EventCommentModel(eventComment)));
        }

        [Route("RemoveEventComment")]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer), HttpPost]
        public IHttpActionResult RemoveEventComment(CommentIdViewModel model)
        {
            var result = _eventCommentService.Remove(model.Id);
            return SuccessResponse(new SuccessModel(result));
        }
    }
}