namespace HomeSim3.Utilities
{
    public static class ImageUploadsExtensions
    {
        public static string SaveImage(this IFormFile formFile, IWebHostEnvironment environment, string folder)
        {
            string path = Path.Combine(environment.WebRootPath, folder);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string fileName = Guid.NewGuid().ToString() + formFile.FileName;
            string fullPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                formFile.CopyToAsync(stream);
            }
            return fileName;
        }
    }
}
