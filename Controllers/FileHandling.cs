using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace The_Book_Shop.Controllers
{
    public  class FileHandling
    {

        private IWebHostEnvironment hostEnvironment;
        public FileHandling(IWebHostEnvironment webHostEnvironment)
        {
            hostEnvironment = webHostEnvironment;
        }

        // handles file upload
        public  String fileUpload(IFormFile image, String folderName)
        {
            String UploadFolder = Path.Combine(hostEnvironment.WebRootPath, "images/"+folderName);

            // creating the file path
            String filepath = Path.Combine(UploadFolder, image.FileName);

            //for storing path to the databse
            String databasePath = image.FileName;

            // checking if the file already exists
            if (System.IO.File.Exists(filepath))
            {
                try
                {
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    System.IO.File.Delete(filepath);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            // copying the file and saving it to the given path on the server
            image.CopyTo(new FileStream(filepath, FileMode.Create));


            return databasePath;
        }

        // deletes the user related file when the file is deleted
        public  bool deleteFile(String image_path, String folderName)
        {
            String ImagesFolder = Path.Combine(hostEnvironment.WebRootPath, "images/"+folderName);

            // creating the file path
            String filepath = Path.Combine(ImagesFolder, image_path);

            if (System.IO.File.Exists(filepath))
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                System.IO.File.Delete(filepath);
                return true;
            }
            else
            {
                return false;
            }

        }

    }
}
