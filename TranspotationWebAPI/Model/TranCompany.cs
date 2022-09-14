using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TranspotationWebAPI.Model
{
    [Table("TranCompany", Schema = "dbo")]
    public class TranCompany
    {
        [Key]                                                   //This will tell this attribute is the Key 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   //This will tell this attribute is not insertable (Tự động tăng dần)
        [Required]
        [Column("companyId", Order = 1)]
        public int companyId { get; set; }

        [Column("name", Order = 2)]
        public String name { get; set; }
        [Column("companyAddress", Order = 3)]
        public String companyAddress { get; set; }
        [Column("contact", Order = 4)]
        public String contact { get; set; }


        public TranCompany(int companyId, string name, string companyAddress, string contact)
        {
            this.companyId = companyId;
            this.name = name;
            this.companyAddress = companyAddress;
            this.contact = contact;
        }
    }
}
