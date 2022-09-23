using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TranspotationAPI.Model;

namespace TranspotationWebAPI.Model
{
    [Table("Order", Schema = "dbo")]
    public class Order
    {
        [Key]                                                   //This will tell this attribute is the Key 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   //This will tell this attribute is not insertable (Tự động tăng dần)
        [Required]
        [Column("orderId" , Order = 1)]
        public Guid orderId { get; set; }       
        
        [Column("totalPrice", Order = 2)]
        public double totalPrice { get; set; }
        
        [Column("createdDate", Order = 3)]
        public DateTime createdDate { get; set; }      
        
        [Column("status", Order = 4)]
        public Boolean status { get; set; }

        //Relationship table
        public Account Account { get; set; }
        public List<SitDetail> SitDetails { get; set; }

        //Contructor
        public Order(Guid orderId, double totalPrice, DateTime createdDate, bool status)
        {
            this.orderId = orderId;           
            this.totalPrice = totalPrice;
            this.createdDate = createdDate;            
            this.status = status;
        }       
    }
}
