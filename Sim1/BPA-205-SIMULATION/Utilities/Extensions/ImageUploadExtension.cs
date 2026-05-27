namespace Simulation_2.Utilities.Extensions
{
    public static class ImageUploadExtension
    {
        public static string SaveImage(this IFormFile imageFile, IWebHostEnvironment web, string folder)
        {
            string path = Path.Combine(web.WebRootPath, folder);
            string fileName = Guid.NewGuid() + imageFile.FileName;
            string fullPath = Path.Combine(path, fileName);
            using(FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                imageFile.CopyTo(stream);
            }
            return fileName;
        }
    }
}
