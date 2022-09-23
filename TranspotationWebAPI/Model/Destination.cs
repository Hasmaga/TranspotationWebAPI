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

        [Column("desAddress", Order = 3)]
        public string desAddress { get; set; }

        // Contructor
        public Destination(int desId, string name, string desAddress)
        {
            this.desId = desId;
            this.name = name;
            this.desAddress = desAddress;
        }        
    }
}