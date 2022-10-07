using System.ComponentModel.DataAnnotations.Schema;
using TranspotationWebAPI.Model.Abstract;

namespace TranspotationWebAPI.Model
{
    [Table("CompanyTrip", Schema ="dbo")]
    public class CompanyTrip : Common
    {
        [Column("TripId")]
        public int? TripId { get; set; }
        public Trip Trip { get; set; }

        [Column("CarId")]
        public int? CarId { get; set; }
        public Car Car { get; set; }


        [Column("CompanyId")]
        public int? CompanyId { get; set; }
        public Company Company { get; set; }

        [Column("Status")]
        public bool? Status { get; set; }

        [Column("StartTime")]
        public DateTime? StartTime { get; set; }

        [Column("Price")]
        public int? Price { get; set; }

        [Column("CarTypeId")]
        public int? CarTypeId { get; set; }
        public CarType CarType { get; set; }

        [Column("StationId")]
        public int? StationId { get; set; }
        public Station Station { get; set; }

        public CompanyTrip(int? TripId, int? CarId, int? CompanyId, bool? Status, DateTime? StartTime, int? Price, int? CarTypeId, int? StationId)
        {
            this.TripId = TripId;
            this.CarId = CarId;
            this.CompanyId = CompanyId;
            this.Status = Status;
            this.StartTime = StartTime;
            this.Price = Price;
            this.CarTypeId = CarTypeId;
            this.StationId = StationId;
        }        
    }
}
