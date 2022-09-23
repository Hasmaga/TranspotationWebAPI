using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TranspotationWebAPI.Model
{
   [Table("Department", Schema = "dbo")]
    public class Depart
    {
        [Key]                                                   //This will tell this attribute is the Key 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   //This will tell this attribute is not insertable (Tự động tăng dần)
        [Required]
        [Column("depId", Order = 1)]
        public int depId { get; set; }

        [Column("name", Order = 2)]
        public String name { get; set; }
        
        [Column("departmetAddress", Order = 3)]
        public String depAddress { get; set; }

        //Contructor
        public Depart(int depId, string name, string depAddress)
        {
            this.depId = depId;
            this.name = name;
            this.depAddress = depAddress;                 
        }        
    }
}