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

        [Column("beginTime", Order = 2)]
        public DateTime? beginTime { get; set; }        

        [Column("price", Order = 3)]
        public int price { get; set; }

        [Column("description", Order = 4)]
        public string? description { get; set; }
        
        //Relationship table        
        public TranCompany TranCompany { get; set; }
        public List<SitDetail> SitDetails { get; set; }        
        public Depart Depart { get; set; }        
        public Destination Destination { get; set; }
        
        //Contructor
        public Trip(int tripId, DateTime? beginTime, int price, string? description)
        {
            this.tripId = tripId;
            this.beginTime = beginTime;
            this.price = price;
            this.description = description;
        }
    }
}

