using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Gift.Api.ViewModel;
using Gift.Core.Services;
using Microsoft.AspNet.Identity;
using AutoMapper;
using Gift.Api.Models;
using Gift.Framework.Utilities.Helpers;
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

        [Route("CreateOrUpdateWithoutImage")]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer), HttpPost]
        public async Task<IHttpActionResult> CreateOrUpdateWithoutImage(GiftItemViewModel model)
        {
            var giftItemParams = new GiftItemParams();
            Mapper.Map(model, giftItemParams);

            /* Get and Assign UserId */
            giftItemParams.UserId = User.Identity.GetUserId<int>();

            //Check If User is updating his/her own comment
            var isUsersOwnEvent = _giftItemService.CheckUserProperty(giftItemParams);

            if (isUsersOwnEvent != null && !isUsersOwnEvent.GetValueOrDefault())
                return ErrorResponse(new ErrorModel(null, Resources.WebApiResource.SavingCommentFailed, 1));

            var giftItemEntity = _giftItemService.CreateOrUpdate(giftItemParams);

            return SuccessResponse(new SuccessModel(giftItemEntity.EventId, null, 0));
        }

        [Route("CreateOrUpdate")]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer), HttpPost]
        public async Task<IHttpActionResult> CreateOrUpdate()
        {
            var virtualPath = "~/img/GiftItem/";

            // Get Multipart Form Data
            var data = await GetMultipartFormData(virtualPath);

            // Deserialize model out of Multipart Form Data
            var model = DeserializeMultipartFormData<GiftItemViewModel>(data);

            // Save image into physical path via virtual path and returns physical file path
            var virtualFilePath = SaveMultipartFormData(data, virtualPath, 400, 400);

            var virtualThumbnailFolderPath = "~/img/GiftItem/Thumbnail/";
            var thumbnailFilePath = CreateThumbnail(virtualFilePath, virtualThumbnailFolderPath, 200, 200);

            if (!ModelState.IsValid)
            {
                return ErrorResponse(new ErrorModel(null, Resources.WebApiResource.RegisterFailed, 1));
            }

            var giftItemParams = new GiftItemParams();
            Mapper.Map(model, giftItemParams);
            giftItemParams.GiftItemImagePath = virtualFilePath;
            giftItemParams.GiftItemThumbnailPath = thumbnailFilePath;

            /* Get and Assign UserId */
            giftItemParams.UserId = User.Identity.GetUserId<int>();
            //If it is update, check if it is user's own event or not
            if (giftItemParams.Id != 0)
            {
                var isUsersOwnEvent = _giftItemService.CheckUserProperty(giftItemParams);
                if (isUsersOwnEvent != null && !isUsersOwnEvent.GetValueOrDefault())
                    return ErrorResponse(new ErrorModel(null, Resources.WebApiResource.CreatingWishlistFailed, 1));
            }

            _giftItemService.CreateOrUpdate(giftItemParams);

            return SuccessResponse(new SuccessModel(giftItemParams.EventId, null, 0));
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

        [Route("Remove")]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer), HttpPost]
        public IHttpActionResult Remove(int giftItemId)
        {            
            return SuccessResponse(new SuccessModel(_giftItemService.Remove(giftItemId)));
        }
    }
}