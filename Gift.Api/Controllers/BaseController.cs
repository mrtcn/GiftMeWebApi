using System;
using System.Net;
using System.IO;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using Gift.Framework.Utilities.Helpers;
using Newtonsoft.Json;
using Gift.Api.Models;
using Gift.Api.Results;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Gift.Api.Controllers
{
    public class BaseController : ApiController
    {
        protected IHttpActionResult ErrorResponse(ErrorModel model, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest) {
            return new ErrorHttpActionResult(this, model, httpStatusCode);
        }

        protected IHttpActionResult SuccessResponse(SuccessModel model)
        {
            return new SuccessHttpActionResult(this, model);
        }

        protected IHttpActionResult UnauthorizedResponse(string loginProvider, UnauthorizedModel model)
        {
            return new ChallengeResult(loginProvider, this, model);
        }

        public string SaveMultipartFormData(MultipartFormDataStreamProvider data, string virtualPath, int width, int height)
        {
            string virtualFilePath = null;
            var replacedAbsoluteUri = Request.RequestUri.AbsoluteUri.Replace(Request.RequestUri.PathAndQuery, string.Empty);

            //get the files and save to the specified path
            foreach (var item in data.FileData)
            {
                var imageName = item.Headers.ContentDisposition.FileName.Replace("\"", "");
                var localFileName = item.LocalFileName;
                var imageHelper = new ImageHelper(imageName, localFileName, virtualPath, replacedAbsoluteUri);
                virtualFilePath = imageHelper.SaveImage(width, height);
            }

            return virtualFilePath;
        }

        public string CreateThumbnail(string virtualFilePath, string virtualThumbnailFolderPath, int width, int height)
        {
            if (virtualFilePath == null)
                return null;

            var replacedAbsoluteUri = Request.RequestUri.AbsoluteUri.Replace(Request.RequestUri.PathAndQuery, string.Empty);
            var imageHelper = new ImageHelper();
            var imagePhysicalPath = imageHelper.GeneratePhysicalFilePath(virtualFilePath);

            var thumbnailUri = imageHelper.GenerateFileUri(virtualThumbnailFolderPath, replacedAbsoluteUri);
            var thumbnailPhysicalPath = imageHelper.GeneratePhysicalFilePath(thumbnailUri.PathAndQuery);

            imageHelper.ResizeImage(imagePhysicalPath, thumbnailPhysicalPath, width, height);

            return thumbnailUri.PathAndQuery;
        }

        public TEntity DeserializeMultipartFormData<TEntity>(MultipartFormDataStreamProvider data) where TEntity : class
        {
            var modelJson = data.FormData["data"];
            var model = JsonConvert.DeserializeObject<TEntity>(modelJson);

            return model;
        }


        public async Task<MultipartFormDataStreamProvider> GetMultipartFormData(string virtualPath)
        {
            var rootPath = HttpContext.Current.Server.MapPath(virtualPath);

            Directory.CreateDirectory(rootPath);
            var provider = new MultipartFormDataStreamProvider(rootPath);
            var data = await Request.Content.ReadAsMultipartAsync(provider);
            if (data.FormData["data"] == null)
            {
                return null;
            }

            return data;
        }
    }
}
