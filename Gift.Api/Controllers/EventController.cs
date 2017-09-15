﻿using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Gift.Api.ViewModel;
using Gift.Core.Services;
using Microsoft.AspNet.Identity;
using AutoMapper;
using Gift.Api.Models;
using Gift.Api.Utilities.Helpers;
using Gift.Core.EntityParams;
using Newtonsoft.Json;

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
            var rootPath = HttpContext.Current.Server.MapPath(virtualPath);

            Directory.CreateDirectory(rootPath);
            var provider = new MultipartFormDataStreamProvider(rootPath);
            var data = await Request.Content.ReadAsMultipartAsync(provider);
            if (data.FormData["data"] == null)
            {
                return ErrorResponse(new ErrorModel(null, Resources.WebApiResource.RegisterFailed, 1));
            }

            var modelJson = data.FormData["data"];
            var model = JsonConvert.DeserializeObject<EventViewModel>(modelJson);

            Uri fileFullPath = null;
            var replacedAbsoluteUri = Request.RequestUri.AbsoluteUri.Replace(Request.RequestUri.PathAndQuery, string.Empty);

            //get the files and save to the specified path
            foreach (var item in data.FileData)
            {
                var imageHelper = new ImageHelper(item, virtualPath, replacedAbsoluteUri);
                fileFullPath = imageHelper.SaveImage();
            }

            if (!ModelState.IsValid)
            {
                return ErrorResponse(new ErrorModel(null, Resources.WebApiResource.RegisterFailed, 1));
            }

            var eventParams = new EventParams();            
            Mapper.Map(model, eventParams);
            eventParams.EventImagePath = fileFullPath?.AbsolutePath;

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