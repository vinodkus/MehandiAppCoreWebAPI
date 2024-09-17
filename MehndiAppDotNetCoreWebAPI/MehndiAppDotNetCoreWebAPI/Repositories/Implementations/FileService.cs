using MehndiAppDotNetCoreWebAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace MehndiAppDotNetCoreWebAPI.Repositories.Implementations
{
    public class FileService : IFileService
    {
        private IWebHostEnvironment environment;
        public FileService(IWebHostEnvironment env)
        {
            this.environment = env;
        }
        public bool DeleteImage(string imageFileName)
        {
            try
            {
                var wwwPath = this.environment.WebRootPath;
                var path = Path.Combine(wwwPath, "Uploads\\", imageFileName);
                if(System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Tuple<int, string> SaveImage(IFormFile imageFile)
        {
            try
            {
                var contentPath = this.environment.ContentRootPath;
                var path = Path.Combine(contentPath,"Uploads");
                if (!Directory.Exists(path))
                { 
                    Directory.CreateDirectory(path);
                }
                var mehndiDesignPath = Path.Combine(path, "MehndiDesigns");
                if (!Directory.Exists(mehndiDesignPath))
                {
                    Directory.CreateDirectory(mehndiDesignPath);
                }
                // Check the allowed extensions
                var ext = Path.GetExtension(imageFile.FileName);
                var allowedExtensions = new string[] { ".jpg",".png",".jpeg"};
                if(!allowedExtensions.Contains(ext))
                {
                    string msg = string.Format("Only {0} extensions are allowed", string.Join(",", allowedExtensions));
                    return new Tuple<int, string>(0, msg);
                }
                string uniqueString = Guid.NewGuid().ToString();
                // we are tying to create a unique filename here
                var newFileName = uniqueString + ext;
                // var fileWithPath = Path.Combine(path, newFileName);
                var fileWithPath = Path.Combine(mehndiDesignPath, newFileName);
                var stream = new FileStream(fileWithPath, FileMode.Create);
                imageFile.CopyTo(stream);
                stream.Close();
                return new Tuple<int, string>(1, newFileName);
            }
            catch (Exception ex)
            {
                return new Tuple<int, string>(0, ex.Message);
            }
        }
    }
}
