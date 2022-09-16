namespace TranspotationWebAPI.Model.Dto
{
    public class GetAllUserInformationResDto
    {
        public int accountId { get; set; }
        public String userName { get; set; }
        public String password { get; set; }
        public Boolean role { get; set; }
        public String phoneNumber { get; set; }
        public String email { get; set; }
        public String name { get; set; }
    }
}
