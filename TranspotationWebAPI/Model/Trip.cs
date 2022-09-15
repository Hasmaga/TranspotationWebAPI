using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TranspotationWebAPI.Model
{
    [Table("Trip", Schema = "dbo")]
    public class Trip
    {
        [Key]                                                   //This will tell this attribute is the Key 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   //This will tell this attribute is not insertable (Tự động tăng dần)
        [Required]
        [Column("Trip_Id", Order = 1)]
        public int tripId { get; set; }

        [Column("Sid_Id", Order = 2)]
        public int sidId { get; set; }

        [Column("Begin Time", Order = 3)]
        public DateTime beginTime { get; set; }

        [Column("End Time", Order = 4)]
        public DateTime endTime { get; set; }

        [Column("Price", Order = 5)]
        public int price { get; set; }

        [Column("Description", Order = 6)]
        public string description { get; set; }

        [Column("Company_Id", Order = 7)]
        public int companyId { get; set; }

        public Trip(int tripId, int sidId, DateTime beginTime, DateTime endTime, int price, string description, int companyId )
        {
            this.tripId = tripId;
            this.sidId = sidId;
            this.beginTime = beginTime;
            this.endTime = endTime;
            this.price = price;
            this.description = description;
            this.companyId = companyId;
        }

        public TranCompany tranCompany { get; set; }
        public SitDetail sitDetail { get; set; }
        public List<Depart> listDepartments { get; set; }
        public List<Destination> listDestinations { get; set; }
    }

}

