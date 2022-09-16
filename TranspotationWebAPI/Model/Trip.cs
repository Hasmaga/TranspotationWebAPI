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
        [Column("tripId", Order = 1)]
        public int tripId { get; set; }        

        [Column("depId", Order = 2)]
        public int depId { get; set; }

        [Column("desId", Order = 3)]
        public int desId { get; set; }

        [Column("beginTime", Order = 4)]
        public DateTime beginTime { get; set; }        

        [Column("price", Order = 5)]
        public int price { get; set; }

        [Column("sescription", Order = 6)]
        public string description { get; set; }

        [Column("companyId", Order = 7)]
        public int companyId { get; set; }

        public Trip(int tripId, int depId, int desId, DateTime beginTime, int price, String description, int companyId)
        {
            this.tripId = tripId;            
            this.depId = depId;
            this.desId = desId;
            this.beginTime = beginTime;
            this.price = price;
            this.description = description;
            this.companyId = companyId;
        }

        public TranCompany tranCompany { get; set; }
        public List<SitDetail> sitDetails { get; set; }
        public List<Depart> listDepartments { get; set; }
        public List<Destination> listDestinations { get; set; }
    }

}

