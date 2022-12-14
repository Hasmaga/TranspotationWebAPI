using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TranspotationWebAPI.Model.Abstract;

namespace TranspotationWebAPI.Model
{
    [Table("Account", Schema = "dbo")]
    public class Account : Common
    {
        [Column("Name")]
        public string Name { get; set; }

        [Column("Phone")]
        public string Phone { get; set; }

        [Column("Email")]
        [RegularExpression(
            @"(http(s)?:\/\/.)?(www\.)?[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)",
            //If regex is not match => send error message, defined as below.
            ErrorMessage = "please enter correct url address"
            )
        ]
        public string Email { get; set; }        
        
        [Column("PasswordHash")]
        public string? PasswordHash { get; set; }

        [Column("PasswordSalt")]
        public string? PasswordSalt { get; set; }

        [Column("Status")]
        public bool Status { get; set; } = true;

        [Column("CompanyId")]
        public int? CompanyId { get; set; }
        public Company Company { get; set; }

        [Column("RoleId")]
        public int RoleId { get; set; }
        public Role Role { get; set; }

        public Account(string name, string phone, string email, bool status, int? companyId, int roleId, string? PasswordHash, string? PasswordSalt )
        {
            this.Name = name;
            this.Phone = phone;
            this.Email = email;            
            this.Status = status;
            this.CompanyId = companyId;
            this.RoleId = roleId;
            this.PasswordHash = PasswordHash;
            this.PasswordSalt = PasswordSalt;
        }
    }
}
