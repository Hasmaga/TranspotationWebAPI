namespace TranspotationWebAPI.Model.Dto
{
    public class ReadCompanyTripResDto
    {
        public int? tripId { get; set; }
        public int? carId { get; set; }
        public bool? status { get; set; }
        public DateTime? startDateTime { get; set; }
        public int? price { get; set; }
        public int? carTypeId { get; set; }
        public int? StationId { get; set; }        
    }
}
