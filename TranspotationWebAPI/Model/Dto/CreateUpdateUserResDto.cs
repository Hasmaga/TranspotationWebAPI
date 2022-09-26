namespace TranspotationWebAPI.Model.Dto
{
    public class CreateUpdateUserResDto
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; } = true;
        public int CompanyId { get; set; } 
        public int roleId { get; set; } = 3;
    }
}
