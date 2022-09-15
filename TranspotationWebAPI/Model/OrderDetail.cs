using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TranspotationAPI.Model;

namespace TranspotationWebAPI.Model
{
    [Table("OrderDetail", Schema = "dbo")]
    public class OrderDetail
    {
        [Key]                                                   //This will tell this attribute is the Key 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   //This will tell this attribute is not insertable (Tự động tăng dần)
        [Required]
        [Column("orderId" , Order = 1)]
        public int orderId { get; set; }

        [Column("accountId" , Order = 2) ]
        public int accountId { get; set; }
        
        [Column("sitDetailId", Order = 3)]
        public int sitDetailId { get; set; }
        
        [Column("totalPrice", Order = 4)]
        public double totalPrice { get; set; }
        
        [Column("createdDate", Order = 5)]
        public DateTime createdDate { get; set; }
        
        [Column("tripId", Order = 6)]
        public int tripId { get; set; }
        
        [Column("status", Order = 7)]
        public Boolean status { get; set; }

        public OrderDetail(int orderId, int accountId, int sitDetailId, double totalPrice, DateTime createdDate, int tripId, bool status)
        {
            this.orderId = orderId;
            this.accountId = accountId;
            this.sitDetailId = sitDetailId;
            this.totalPrice = totalPrice;
            this.createdDate = createdDate;
            this.tripId = tripId;
            this.status = status;

        }     
        public Account account { get; set; }
        public List<SitDetail> sitDetails { get; set; }

    }
}
