namespace TranspotationWebAPI.Model.Dto
{
    public class CreateUpdateCompanyTripResDto
    {
        public int TripId { get; set; }
        public int CarId { get; set; }        
        public bool Status { get; set; }        
        public DateTime? StartDateTime { get; set; }
        public int Price { get; set; }
        public int CarTypeId { get; set; }
        public int StationId { get; set; }
        public int TotalSeat { get; set; }
    }
}
