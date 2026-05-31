namespace HomeSim4.Utilities
{
    public static class ImageUploadsExtensions
    {
        public static string SaveImage(this IFormFile imageFile, IWebHostEnvironment env, string folder)
        {
            string path = Path.Combine(env.WebRootPath, folder);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
            string fullPath = Path.Combine(path, fileName);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                imageFile.CopyToAsync(stream);
            }
            return fileName;
        }
    }
}
