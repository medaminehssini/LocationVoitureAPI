namespace LocationVoitureApi.Models
{
    public class AdminUpload
    {
        public int Id { get; set; }
        public string? Nom { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
