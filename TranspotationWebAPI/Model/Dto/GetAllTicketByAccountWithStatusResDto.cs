namespace TranspotationWebAPI.Model.Dto
{
    public class GetAllTicketByAccountWithStatusResDto
    {
        public bool Status { get; set; }
        public double? Total { get; set; }
        public string CompanyName { get; set; }
        public string CarTypeName { get; set; }
        public DateTime? StartDateTime { get; set; }
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }
        public string SeatName { get; set; }
        public string Description { get; set; }
    }
}
