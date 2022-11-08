namespace TranspotationWebAPI.Model.Dto
{
    public class GetCompanyTripByTripIdResDto
    {
        public string? CompanyName { get; set; }
        public DateTime? StartDateTime { get; set; }
        public int? Price { get; set; }
        public string? CarTypeName { get; set; }
        public string? StationName { get; set; }
    }
}
