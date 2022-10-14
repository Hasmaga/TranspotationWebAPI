namespace TranspotationWebAPI.Model.Dto
{
    public class UpdateTicketByTokenResDto
    {
        public bool Status { get; set; }
        public int? Total { get; set; }      
        public string? SeatName { get; set; }
        public string? Description { get; set; }
    }
}
