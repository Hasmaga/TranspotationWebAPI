namespace TranspotationAPI.Model.Dto
{
    // Dto for Get User Information By Id
    public class GetUserInformationResDto
    {
        public int accountId { get; set; }
        public String userName { get; set; }
        public String phoneNumber { get; set; }
        public String email { get; set; }
    }
}
