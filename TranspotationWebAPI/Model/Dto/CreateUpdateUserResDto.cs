namespace TranspotationWebAPI.Model.Dto
{
    public class CreateUpdateUserResDto
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }        
        public bool Status { get; set; } = true;
        public int CompanyId { get; set; } = 0;
        public int roleId { get; set; } = 3;
        public string? PasswordHash { get; set; } 
        public string? PasswordSalt { get; set; }        
    }
}
