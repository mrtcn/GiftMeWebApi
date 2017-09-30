using System;
using System.IO;
using System.Web;
using ImageResizer;

namespace Gift.Framework.Utilities.Helpers
{
    public class ImageHelper {
        private string ImageName { get; }
        private string LocalFileName { get; }
        private string VirtualPath { get; }
        private string ReplacedAbsoluteUri { get; }

        public ImageHelper()
        {

        }

        public ImageHelper(string imageName, string localFileName, string virtualPath, string replacedAbsoluteUri)
        {
            ImageName = imageName;
            LocalFileName = localFileName;
            VirtualPath = virtualPath;
            ReplacedAbsoluteUri = replacedAbsoluteUri;
        }

        public string SaveImage(int width, int height)
        {            
            var fileUri = GenerateFileUri(VirtualPath, ReplacedAbsoluteUri);

            var physicalSavedPath = GeneratePhysicalFilePath(fileUri.PathAndQuery);

            File.Move(LocalFileName, physicalSavedPath);

            ResizeImage(physicalSavedPath, physicalSavedPath, width, height);

            return fileUri.PathAndQuery;
        }

        public void ResizeImage(string physicalImagePath, string physicalDestinationPath, int width, int height)
        {
            var instructions = new Instructions() {
                Width = width,
                Height = height,
                Scale = ScaleMode.DownscaleOnly
            };
            ImageJob i = new ImageJob(physicalImagePath, physicalDestinationPath, instructions);
            i.CreateParentDirectory = true;
            i.Build();
        }

        public Uri GenerateFileUri(string virtualPath, string replacedAbsoluteUri)
        {
            var rootPath = HttpContext.Current.Server.MapPath(virtualPath);
            var extension = Path.GetExtension(ImageName);
            string newFileName = Guid.NewGuid() + (extension?? ".jpeg");

            Uri baseuri = new Uri(replacedAbsoluteUri);
            string fileRelativePath = virtualPath + newFileName;
            Uri fileUri = new Uri(baseuri, VirtualPathUtility.ToAbsolute(fileRelativePath));

            return fileUri;
        }

        public string GeneratePhysicalFilePath(string virtualFilePath)
        {
            var physicalSavedPath = HttpContext.Current.Server.MapPath(virtualFilePath);

            return physicalSavedPath;
        }
    }
}
