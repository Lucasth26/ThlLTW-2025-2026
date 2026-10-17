namespace NetCoreLA7THL.Helpers
{
    public static class THLFileHelper
    {
        private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };

        private const long MaxLength = 2 * 1024 * 1024;

        public static bool IsValidImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return true;
            }
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            return AllowedExtensions.Contains(ext) && file.Length <= MaxLength;
        }

        public static async Task<string> SaveImageAsync(IFormFile file, IWebHostEnvironment env, string folder)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }
            var dir = Path.Combine(env.WebRootPath, "uploads", folder);
            Directory.CreateDirectory(dir);
            var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName).ToLowerInvariant();
            using (var stream = new FileStream(Path.Combine(dir, fileName), FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return "/uploads/" + folder + "/" + fileName;
        }

        public static void DeleteFile(string relativePath, IWebHostEnvironment env)
        {
            if (string.IsNullOrWhiteSpace(relativePath))
            {
                return;
            }
            var full = Path.Combine(env.WebRootPath, relativePath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
            if (File.Exists(full))
            {
                File.Delete(full);
            }
        }
    }
}