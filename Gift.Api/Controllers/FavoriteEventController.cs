using System.Web.Http;
using Gift.Api.Models;
using Gift.Api.ViewModel;
using Gift.Core.EntityParams;
using Gift.Core.Services;
using Microsoft.AspNet.Identity;

namespace Gift.Api.Controllers
{
    [RoutePrefix("api/Favorite"), Authorize]
    public class FavoriteEventController : BaseController
    {
        private readonly IFavoriteEventService _favoriteEventService;

        public FavoriteEventController(
            IFavoriteEventService favoriteEventService)
        {
            _favoriteEventService = favoriteEventService;
        }

        [Route("FavoriteEventList")]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer), HttpPost]
        public IHttpActionResult FavoriteEventList()
        {
            /* Get and Assign UserId */
            var userId = User.Identity.GetUserId<int>();

            var favoriteEvents = _favoriteEventService.FavoriteEventList(userId);
            return SuccessResponse(new SuccessModel(favoriteEvents));
        }

        [Route("AddOrUpdateFavoriteEvent")]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer), HttpPost]
        public IHttpActionResult AddOrUpdateFavoriteEvent(EventIdModel model)
        {
            var favoriteEventParams = new FavoriteEventParams(model.EventId);
            AutoMapper.Mapper.Map(model, favoriteEventParams);            

            /* Get and Assign UserId */
            favoriteEventParams.UserId = User.Identity.GetUserId<int>();

            var favoriteEventModel = _favoriteEventService.GetFavoriteEvent(favoriteEventParams.UserId, model.EventId);

            if (favoriteEventModel == null) {
                favoriteEventParams.Status = Data.Models.Status.Active;
                _favoriteEventService.CreateOrUpdate(favoriteEventParams);
                return SuccessResponse(new SuccessModel(true));
            } else {
                _favoriteEventService.Remove(favoriteEventModel.Id);
                return SuccessResponse(new SuccessModel(false));
            }
        }

        [Route("RemoveFavoriteEvent")]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer), HttpPost]
        public IHttpActionResult RemoveFavoriteEvent(EventIdModel model)
        {
            _favoriteEventService.Remove(model.EventId);
            return SuccessResponse(new SuccessModel(null));
        }        
    }
}