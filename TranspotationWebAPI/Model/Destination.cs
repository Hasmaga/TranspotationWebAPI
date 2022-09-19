using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TranspotationWebAPI.Model
{
   [Table("Destination", Schema = "dbo")]
    public class Destination
    {
        [Key]                                                   //This will tell this attribute is the Key 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   //This will tell this attribute is not insertable (Tự động tăng dần)
        [Required]
        [Column("desId", Order = 1)]
        public int desId { get; set; }

        [Column("name", Order = 2)]
        public string name { get; set; }

        [Column("destinationAddress", Order = 3)]
        public string destinationAddress { get; set; }

        public Destination(int desId, string name, string destinationAddress)
        {
            this.desId = desId;
            this.name = name;
            this.destinationAddress = destinationAddress;
        }        
    }
}