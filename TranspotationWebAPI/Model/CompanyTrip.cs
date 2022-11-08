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

        [Column("StartDateTime")]
        public DateTime? StartDateTime { get; set; }       

        [Column("Price")]
        public int? Price { get; set; }

        [Column("CarTypeId")]
        public int? CarTypeId { get; set; }
        public CarType CarType { get; set; }

        [Column("StationId")]
        public int? StationId { get; set; }
        public Station Station { get; set; }

        [Column("TotalSeat")]
        public int? TotalSeat { get; set; }       

        public CompanyTrip(int? tripId, int? carId, int? companyId, bool? status, DateTime? startDateTime, int? price, int? carTypeId, int? stationId, int? totalSeat)
        {
            this.TripId = tripId;
            this.CarId = carId;
            this.CompanyId = companyId;
            this.Status = status;
            this.StartDateTime = startDateTime;
            this.Price = price;
            this.CarTypeId = carTypeId;
            this.StationId = stationId;
            this.TotalSeat = totalSeat;            
        }        
    }
}
