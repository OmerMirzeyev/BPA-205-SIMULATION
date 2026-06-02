namespace Sim4.Utilities
{
    public static class ImageUploadsExtensions
    {
        public static string SaveImage(this IFormFile formFile, IWebHostEnvironment env , string folder)
        {
            string path = Path.Combine(env.WebRootPath, folder);
            string fileName = Guid.NewGuid().ToString() + "_" + formFile.FileName;
            string fullPath = Path.Combine(path, fileName);

            using(FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                formFile.CopyTo(stream);
            }
            return fileName;
        }
    }
}
