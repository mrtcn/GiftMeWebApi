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
using Gift.Api.Utilities.Helpers;
using Gift.Core.EntityParams;
using Newtonsoft.Json;

namespace Gift.Api.Controllers
{
    [RoutePrefix("api/GiftItem"), Authorize]
    public class GiftItemController : ApiController
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
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            var virtualPath = "~/App_Data/Temp/FileUploads/Register";
            var rootPath = HttpContext.Current.Server.MapPath(virtualPath);

            Directory.CreateDirectory(rootPath);
            var provider = new MultipartFormDataStreamProvider(rootPath);
            var data = await Request.Content.ReadAsMultipartAsync(provider);
            if (data.FormData["data"] == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
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
                return BadRequest(ModelState);
            }
            var giftItemParams = new GiftItemParams();            
            Mapper.Map(model, giftItemParams);
            giftItemParams.GiftImagePath = fileFullPath?.AbsoluteUri;
            /* Get and Assign UserId */
            giftItemParams.UserId = User.Identity.GetUserId<int>();
            //If it is update, check if it is user's own giftItem or not
            if (giftItemParams.Id != 0)
            {
                var isUsersOwnGiftItem = _giftItemService.CheckUserProperty(giftItemParams);
                if (!isUsersOwnGiftItem)
                    return BadRequest("Failure");
            }
            //CreatedBy and UpdatedBy 
            if (giftItemParams.Id > 0)
            {
                
            }
            _giftItemService.CreateOrUpdate(giftItemParams);
                             
            return Ok(giftItemParams.Id);
        }

        [Route("GiftItemList")]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer), HttpPost]
        public IHttpActionResult GiftItemListByEventId(GiftItemIdModel model)
        {
            var giftItemList = _giftItemService.GiftItemListByEventId(model.GiftItemId);
            return Ok(giftItemList);
        }

        [Route("GetGiftItemById")]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer), HttpPost]
        public IHttpActionResult GetGiftItemById(GiftItemIdModel giftItemModel)
        {
            var giftItemDetail = new GiftItemModel(_giftItemService.Get(giftItemModel.GiftItemId));
            return Ok(giftItemDetail);
        }

        [Route("RemoveGiftItem")]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer), HttpPost]
        public IHttpActionResult RemoveGiftItem(int giftItemId)
        {
            return Ok(_giftItemService.Remove(giftItemId));
        }
    }
}