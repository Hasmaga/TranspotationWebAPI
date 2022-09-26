namespace TranspotationWebAPI.Model.Dto
{
    public class GetAllUserInformationResDto
    {
        public int Id { get; set; }
        public String password { get; set; }        
        public String phoneNumber { get; set; }
        public String email { get; set; }
        public String name { get; set; }
        public bool status { get; set; }
        public String roleName { get; set; }
        public String roleAuthority { get; set; }        
    }
}
