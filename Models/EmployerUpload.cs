namespace LocationVoitureApi.Models
{
    public class EmployerUpload 
    {

        public int Id { get; set; }
        public string? Nom { get; set; }
        public string? Password { get; set; }
        public IFormFile photo { get; set; }
        public int Idagence { get; set; }
    }
}
