using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TranspotationWebAPI.Model;

namespace TranspotationAPI.Model
{
    [Table("Account", Schema = "dbo")]
    public class Account
    {
        [Key]                                                   //This will tell this attribute is the Key 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   //This will tell this attribute is not insertable (Tự động tăng dần)
        [Required]                                              //This will tell this attribute is Required (Ko thể null) 
        [Column("accountId", Order = 1)]                        //This will tell the attribute's Name
        public int accountId { get; set; }        
        
        [Column("userName", Order = 2)]
        public String userName { get; set; }        
        
        [Column("password", Order = 3)]
        public String password { get; set; }        
        
        [Column("role", Order = 4)]
        public Boolean role { get; set; }        
        
        [Column("phoneNumber", Order = 5)]
        public String phoneNumber { get; set; }

        [RegularExpression(
            @"(http(s)?:\/\/.)?(www\.)?[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)",
            //If regex is not match => send error message, defined as below.
            ErrorMessage = "please enter correct url address"
            )
        ]
        [Column("email", Order = 6)]
        public String email { get; set; }        
        
        [Column("name", Order = 7)]
        public String name { get; set; }
        public Account(int accountId, string userName, string password, bool role, string phoneNumber, string email, string name)
        {
            this.accountId = accountId;
            this.userName = userName;
            this.password = password;
            this.role = role;
            this.phoneNumber = phoneNumber;
            this.email = email;
            this.name = name;
        }
        public List<OrderDetail> orderDetails { get; set; }
    }
}
