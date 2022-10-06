namespace TranspotationWebAPI.Model.Dto
{
    public class GetTicketByAccountResDto
    {
        public bool Status { get; set; }
        public int? Total { get; set; }        
        public int? CompanyTripId { get; set; }
        public string? SeatName { get; set; }
        public string? Description { get; set; }        
    }
}
