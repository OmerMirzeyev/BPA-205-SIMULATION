namespace HomeSim2.Utilities
{
    public static class FileUploadsExtensions
    {
        public static string SaveImage(this IFormFile formFile, IWebHostEnvironment environment, string folder)
        {
            string path = Path.Combine(environment.WebRootPath, folder);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fileName = Guid.NewGuid().ToString() + "_" + formFile.FileName;
            string fullPath = Path.Combine(path, fileName);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                formFile.CopyTo(stream);
            }

            return fileName;
        }
    }
}
