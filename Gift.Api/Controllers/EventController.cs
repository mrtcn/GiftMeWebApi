using System;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http;
using Gift.Api.ViewModel;
using Gift.Core.Services;
using Microsoft.AspNet.Identity;
using AutoMapper;
using Gift.Api.Models;
using Gift.Core.EntityParams;

namespace Gift.Api.Controllers
{
    [RoutePrefix("api/Event"), Authorize]
    public class EventController : BaseController
    {
        private readonly IEventService _eventService;

        public EventController(
            IEventService eventService)
        {
            _eventService = eventService;
        }

        [Route("CreateOrUpdate")]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        public async Task<IHttpActionResult> CreateOrUpdate()
        {
            var virtualPath = "~/img/Events/";

            // Get Multipart Form Data
            var data = await GetMultipartFormData(virtualPath);

            // Deserialize model out of Multipart Form Data
            var model = DeserializeMultipartFormData<EventViewModel>(data);

            // Save image into physical path via virtual path and returns physical file path
            var virtualFilePath = SaveMultipartFormData(data, virtualPath, 400, 400);

            var virtualThumbnailFolderPath = "~/img/Events/Thumbnail/";
            var thumbnailFilePath = CreateThumbnail(virtualFilePath, virtualThumbnailFolderPath, 200, 200);

            if (!ModelState.IsValid)
            {
                return ErrorResponse(new ErrorModel(null, Resources.WebApiResource.RegisterFailed, 1));
            }

            var eventParams = new EventParams();            
            Mapper.Map(model, eventParams);
            eventParams.EventImagePath = virtualFilePath;
            eventParams.EventThumbnailPath = thumbnailFilePath;

            /* Get and Assign UserId */
            eventParams.UserId = User.Identity.GetUserId<int>();
            //If it is update, check if it is user's own event or not
            if (eventParams.Id != 0)
            {
                var isUsersOwnEvent = _eventService.CheckUserProperty(eventParams);
                if (isUsersOwnEvent != null && !isUsersOwnEvent.GetValueOrDefault())
                    return ErrorResponse(new ErrorModel(null, Resources.WebApiResource.CreatingWishlistFailed, 1));
            }

            _eventService.CreateOrUpdate(eventParams);

            return SuccessResponse(new SuccessModel(eventParams.Id, null, 0));
        }

        [Route("CreateOrUpdateWithoutImage")]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer), HttpPost]
        public IHttpActionResult CreateOrUpdateWithoutImage(EventViewModel model)
        {
            var eventParams = new EventParams();
            Mapper.Map(model, eventParams);

            /* Get and Assign UserId */
            eventParams.UserId = User.Identity.GetUserId<int>();

            //Check If User is updating his/her own comment
            var isUsersOwnEvent = _eventService.CheckUserProperty(eventParams);

            if (isUsersOwnEvent != null && !isUsersOwnEvent.GetValueOrDefault())
                return ErrorResponse(new ErrorModel(null, Resources.WebApiResource.SavingCommentFailed, 1));

            var eventEntity = _eventService.CreateOrUpdate(eventParams);

            return SuccessResponse(new SuccessModel(eventEntity.Id, null, 0));
        }

        [Route("EventList")]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer), HttpPost]
        public IHttpActionResult EventList(EventListTypeModel model)
        {
            throw new Exception();
            var userId = User.Identity.GetUserId<int>();
            var eventList = _eventService.EventList(model.EventListType, userId, model.SearchTerm);
            return SuccessResponse(new SuccessModel(eventList, null, 0));
        }

        [Route("GetEventById")]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer), HttpPost]
        public IHttpActionResult GetEventById(EventIdModel eventModel)
        {
            var userId = User.Identity.GetUserId<int>();
            var eventDetail = _eventService.GetEventById(eventModel.EventId, userId);
            return SuccessResponse(new SuccessModel(eventDetail, null, 0));
        }

        [Route("RemoveEvent")]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer), HttpPost]
        public IHttpActionResult RemoveEvent(int eventId)
        {
            return SuccessResponse(new SuccessModel(_eventService.Remove(eventId), null, 0));
        }
    }
}