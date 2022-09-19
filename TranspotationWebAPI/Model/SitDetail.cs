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

        [Column("trip_Id", Order = 2)]
        public int trip_Id { get; set; }
        [Column("name", Order = 3)]
        public String Name { get; set; }
        [Column("status", Order = 4)]
        public Boolean status { get; set; }
        public SitDetail(int sitId, int trip_Id, string name, bool status)
        {
            this.sitId = sitId;
            this.trip_Id = trip_Id;
            Name = name;
            this.status = status;
        }
        public Order order { get; set; }        
        public Trip trip { get; set; }
    }    
}
