namespace LocationVoitureApi.Helpers
{
    public interface IUpload
    {

        public string upload(IFormFile file, string url);
    }
}
