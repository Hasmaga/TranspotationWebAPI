namespace TranspotationWebAPI.Model.Dto
{
    public class GetUserInformationResDto
    {
        public int Id { get; set; }        
        public String phoneNumber { get; set; }
        public String email { get; set; }
        public String name { get; set; }
        public bool status { get; set; }
        public String roleName { get; set; }
        public String roleAuthority { get; set; }
        public String passwordHash { get; set; }
        public String passwordSalt { get; set; }
    }
}
