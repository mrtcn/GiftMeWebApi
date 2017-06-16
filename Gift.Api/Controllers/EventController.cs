using System.Linq;
using System.Web.Http;
using Gift.Api.ViewModel;
using Gift.Core.Services;
using Microsoft.AspNet.Identity;
using AutoMapper;
using Gift.Core.EntityParams;

namespace Gift.Api.Controllers
{
    [RoutePrefix("api/Event"), Authorize]
    public class EventController : ApiController
    {
        private readonly IEventService _eventService;

        public EventController(
            IEventService eventService)
        {
            _eventService = eventService;
        }

        [Route("CreateOrUpdateEvent")]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer), HttpPost]
        public IHttpActionResult CreateOrUpdateEvent(EventViewModel model)
        {
            var eventParams = new EventParams();            
            Mapper.Map(model, eventParams);

            /* Get and Assign UserId */
            eventParams.UserId = User.Identity.GetUserId<int>();

            var isUsersOwnEvent = _eventService.CheckUserProperty(eventParams);
            if (!isUsersOwnEvent)
                return BadRequest("Failure");

            _eventService.CreateOrUpdate(eventParams);
                             
            return Ok();
        }

        [Route("EventList")]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer), HttpPost]
        public IHttpActionResult EventList(EventListModel model)
        {
            var userId = User.Identity.GetUserId<int>();
            var eventList = _eventService.EventList(model.EventListType, userId);
            return Ok(eventList);
        }

        [Route("GetEventById")]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer), HttpPost]
        public IHttpActionResult GetEventById(EventIdModel eventModel)
        {
            var eventDetail = new EventModel(_eventService.Get(eventModel.EventId));
            return Ok(eventDetail);
        }

        [Route("RemoveEvent")]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer), HttpPost]
        public IHttpActionResult RemoveEvent(int eventId)
        {
            return Ok(_eventService.Remove(eventId));
        }
    }
}