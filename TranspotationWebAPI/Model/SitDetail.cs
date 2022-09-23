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
        
        [Column("name", Order = 2)]
        public String name { get; set; }
        [Column("status", Order = 3)]
        public Boolean? status { get; set; }        

        //Contructor
        public SitDetail(int sitId, string name, Boolean? status)
        {
            this.sitId = sitId;            
            this.name = name;
            this.status = status;
        }
        
    }    
}
