using System;
using System.IO;
using System.Net.Http;
using System.Web;

namespace Gift.Api.Utilities.Helpers
{
    public class ImageHelper {
        private MultipartFileData Item { get; }
        private string VirtualPath { get; }
        private string ReplacedAbsoluteUri { get; }
        public ImageHelper(MultipartFileData item, string virtualPath, string replacedAbsoluteUri)
        {
            Item = item;
            VirtualPath = virtualPath;
            ReplacedAbsoluteUri = replacedAbsoluteUri;        
        }
        public Uri SaveImage()
        {
            var rootPath = HttpContext.Current.Server.MapPath(VirtualPath);
            string name = Item.Headers.ContentDisposition.FileName.Replace("\"", "");
            string newFileName = Guid.NewGuid() + Path.GetExtension(name);
            string fullSavedPath = Path.Combine(rootPath, newFileName);
            File.Move(Item.LocalFileName, fullSavedPath);
            Uri baseuri = new Uri(ReplacedAbsoluteUri);
            string fileRelativePath = VirtualPath + newFileName;
            Uri fileFullPath = new Uri(baseuri, VirtualPathUtility.ToAbsolute(fileRelativePath));
            return fileFullPath;
        }
    }
}
