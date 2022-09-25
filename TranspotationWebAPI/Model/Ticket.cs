using System.ComponentModel.DataAnnotations.Schema;
using TranspotationWebAPI.Model.Abstract;

namespace TranspotationWebAPI.Model
{
    [Table("Ticket", Schema = "dbo")]
    public class Ticket : Common
    {
        [Column("Status")]
        public bool Status { get; set; }

        [Column("Total")]
        public int Total { get; set; }

        [Column("AccountId")]
        public int AccountId { get; set; }
        public Account Account { get; set; }

        [Column("CompanyTripId")]
        public int CompanyTripId { get; set; }
        public CompanyTrip CompanyTrip { get; set; }

        [Column("SeatName")]
        public string SeatName { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        public Ticket(bool Status, int Total, int AccountId, int CompanyTripId, string SeatName, string Description)
        {
            this.Status = Status;
            this.Total = Total;
            this.AccountId = AccountId;
            this.CompanyTripId = CompanyTripId;
            this.SeatName = SeatName;
            this.Description = Description;        
        }        
    }
}
