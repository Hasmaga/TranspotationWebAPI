using System.ComponentModel.DataAnnotations.Schema;
using TranspotationWebAPI.Model.Abstract;

namespace TranspotationWebAPI.Model
{
    [Table("Company", Schema = "dbo")]
    public class Company : Common
    {
        [Column("Name")]
        public string Name { get; set; }

        [Column("Phone")]
        public string Phone { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        [Column("Address")]
        public string Address { get; set; }

        public Company (string Name, string Phone, string Email, string Address)
        {
            this.Name = Name;
            this.Phone = Phone;
            this.Email = Email;
            this.Address = Address;
        }
    }
}
