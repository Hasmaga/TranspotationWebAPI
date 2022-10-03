namespace TranspotationWebAPI.Model.Dto
{
    public class CreateAccountResDto
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }        
        public int CompanyId { get; set; }
        public int RoleId { get; set; }
    }
}
