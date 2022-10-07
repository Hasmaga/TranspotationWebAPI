using System.Security.Cryptography.X509Certificates;

namespace TranspotationWebAPI.Model.Dto
{
    public class UpdateInfoUserResDto
    {
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
