using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace SharedLibrary.Models
{
    public abstract class MediaModel
    {

        public abstract IFormFile media { get; }
        public abstract string path { get; }

        public string OutputPath { get; set; } = "";
        public void SaveMedia(IHostingEnvironment environment)
        {
            // Check if the request contains multipart/form-data.
            if (media == null)
            {
                return;
            }

            else if (media.Length > 0)
            {
                IFormFile formFile = media;

                var folderPath = Path.Combine(environment.WebRootPath, path);
                //var filePath = Path.Combine(folderPath, $"{Path.GetRandomFileName() + Path.GetExtension(formFile.FileName).ToLowerInvariant()}");

                var filePath = Path.Combine(folderPath, formFile.FileName);
                var storeDbPath = Path.Combine(path, formFile.FileName);

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                for (var i = 0; File.Exists(filePath); i++)
                {
                    filePath = Path.Combine(folderPath, i.ToString() + "_" + formFile.FileName);
                    storeDbPath = Path.Combine(path, i.ToString() + "_" + formFile.FileName);
                }

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    formFile.CopyTo(fileStream);
                    fileStream.Flush();
                    this.OutputPath = storeDbPath;
                }
            }
        }
    }
}

