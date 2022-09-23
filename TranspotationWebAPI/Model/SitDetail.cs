using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TranspotationWebAPI.Model
{
    [Table("SitDetail", Schema = "dbo")]
    public class SitDetail
    {
        [Key]                                                   //This will tell this attribute is the Key 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   //This will tell this attribute is not insertable (Tự động tăng dần)
        [Required]
        [Column("sitId", Order = 1)]
        public int sitId { get; set; }
        
        [Column("name", Order = 3)]
        public String name { get; set; }
        [Column("status", Order = 4)]
        public Boolean status { get; set; }
        //Relationship Table
        public Order order { get; set; }        
        public Trip trip { get; set; }

        //Contructor
        public SitDetail(int sitId, string name, bool status)
        {
            this.sitId = sitId;            
            this.name = name;
            this.status = status;
        }
        
    }    
}
