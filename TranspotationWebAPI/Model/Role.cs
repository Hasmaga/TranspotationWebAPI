using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TranspotationWebAPI.Model
{
    [Table("Role", Schema = "dbo")]
    public class Role
    {
        [Key]                                                   //This will tell this attribute is the Key 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   //This will tell this attribute is not insertable (Tự động tăng dần)
        [Required]
        [Column("roleId", Order = 1)]
        public int roleId { get; set; }
        [Column("roleName", Order = 2)]
        public String roleName { get; set; }

        //Contructor
        public Role(int roleId, string roleName)
        {
            this.roleId = roleId;
            this.roleName = roleName;
        }
    }
}
