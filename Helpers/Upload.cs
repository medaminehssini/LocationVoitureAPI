namespace LocationVoitureApi.Helpers
{
    public class Upload : IUpload
    {
        private readonly IWebHostEnvironment hostEnvironment;

        public Upload (IWebHostEnvironment hostEnvironment )
        {
            this.hostEnvironment = hostEnvironment;
        }

        public string upload(IFormFile file, string url)
        {
            string uniqueFileName = null;

            if (file != null)
            {
                string uploadsFolder = Path.Combine(hostEnvironment.WebRootPath , url);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
