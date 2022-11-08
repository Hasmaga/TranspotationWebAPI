namespace TranspotationWebAPI.Model.Dto
{
    public class GetTicketByTicketId
    {
        public int TicketId { get; set; }
        public bool Status { get; set; }
        public int? Total { get; set; }
        public string CompanyName { get; set; }        
        public string SeatName { get; set; }
        public string Description { get; set; }
        public string LocationFrom { get; set; }
        public string LocationTo { get; set; }
        public DateTime? StartDateTime { get; set; }
        public string CarTypeName { get; set; }
    }
}
