using System;
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
    [RoutePrefix("api/GiftItem"), Authorize]
    public class GiftItemController : BaseController
    {
        private readonly IGiftItemService _giftItemService;

        public GiftItemController(
            IGiftItemService giftItemService)
        {
            _giftItemService = giftItemService;
        }

        [Route("CreateOrUpdateGiftItem")]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer), HttpPost]
        public async Task<IHttpActionResult> CreateOrUpdateGiftItem()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                return ErrorResponse(new ErrorModel(null, Resources.WebApiResource.UnsupportedMediaType, 1), HttpStatusCode.UnsupportedMediaType);
            }
            var virtualPath = "~/Register";
            var rootPath = HttpContext.Current.Server.MapPath(virtualPath);

            Directory.CreateDirectory(rootPath);
            var provider = new MultipartFormDataStreamProvider(rootPath);
            var data = await Request.Content.ReadAsMultipartAsync(provider);
            if (data.FormData["data"] == null)
            {
                return ErrorResponse(new ErrorModel(null, Resources.WebApiResource.CreatingItemFailed, 1));
            }

            var modelJson = data.FormData["data"];
            var model = JsonConvert.DeserializeObject<GiftItemViewModel>(modelJson);

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
                return ErrorResponse(new ErrorModel(null, Resources.WebApiResource.CreatingItemFailed, 1));
            }
            var giftItemParams = new GiftItemParams();            
            Mapper.Map(model, giftItemParams);
            giftItemParams.GiftImagePath = fileFullPath?.AbsolutePath;
            /* Get and Assign UserId */
            giftItemParams.UserId = User.Identity.GetUserId<int>();
            //If it is update, check if it is user's own giftItem or not
            if (giftItemParams.Id != 0)
            {
                var isUsersOwnGiftItem = _giftItemService.CheckUserProperty(giftItemParams);
                if (isUsersOwnGiftItem != null && !isUsersOwnGiftItem.GetValueOrDefault())
                    return ErrorResponse(new ErrorModel(null, Resources.WebApiResource.CreatingItemFailed, 1));
            }
            //CreatedBy and UpdatedBy 
            if (giftItemParams.Id > 0)
            {
                
            }
            _giftItemService.CreateOrUpdate(giftItemParams);
                             
             return SuccessResponse(new SuccessModel(giftItemParams.Id));
        }

        [Route("ToggleGiftItemBuyStatus")]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer), HttpPost]
        public IHttpActionResult ToggleGiftItemBuyStatus(ToggleBuyStatusModel model)
        {
            var giftItem = _giftItemService.Get(model.Id);

            var giftItemParams = new GiftItemParams();
            Mapper.Map(giftItem, giftItemParams);

            /* Get and Assign UserId */
            giftItemParams.UserId = User.Identity.GetUserId<int>();
            giftItemParams.IsBought = model.IsBought;

            giftItem = _giftItemService.CreateOrUpdate(giftItemParams);
            return SuccessResponse(new SuccessModel(new GiftItemModel(giftItem)));
        }
        
        [Route("GiftItemList")]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer), HttpPost]
        public IHttpActionResult GiftItemListByEventId(EventIdModel model)
        {
            var userId = User.Identity.GetUserId<int>();
            var giftItemList = _giftItemService.GiftItemListByEventId(model.EventId, userId);
            return SuccessResponse(new SuccessModel(giftItemList));
        }

        [Route("GetGiftItemById")]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer), HttpPost]
        public IHttpActionResult GetGiftItemById(GiftItemIdModel giftItemModel)
        {
            var giftItemDetail = new GiftItemModel(_giftItemService.Get(giftItemModel.GiftItemId));

            return SuccessResponse(new SuccessModel(giftItemDetail));
        }

        [Route("RemoveGiftItem")]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer), HttpPost]
        public IHttpActionResult RemoveGiftItem(int giftItemId)
        {            
            return SuccessResponse(new SuccessModel(_giftItemService.Remove(giftItemId)));
        }
    }
}