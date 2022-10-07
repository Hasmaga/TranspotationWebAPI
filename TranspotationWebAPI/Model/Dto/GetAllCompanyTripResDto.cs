﻿namespace TranspotationWebAPI.Model.Dto
{
    public class GetAllCompanyTripResDto
    {
        public string? LocationFrom { get; set; }
        public string? LoactionTo { get; set; }
        public string? CarName { get; set; }
        public string? CompanyName { get; set; }
        public DateTime? StartTime { get; set; }
        public int? Price { get; set; }
        public string? CarTypeName { get; set; }
        public string? StationName { get; set; }
    }
}
