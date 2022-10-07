using System.ComponentModel.DataAnnotations.Schema;
using TranspotationWebAPI.Model.Abstract;

namespace TranspotationWebAPI.Model
{
    [Table("Role", Schema = "dbo")]
    public class Role : Common
    {
        [Column("Name")]
        public string Name { get; set; }
        
        [Column("Authority")]
        public string Authority { get; set; }

        public Role(string name, string authority)
        {
            this.Name = name;
            this.Authority = authority;
        }
    }
}
