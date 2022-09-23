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
        public Guid accountId { get; set; }        
        
        [Column("userName", Order = 2)]
        public String userName { get; set; }        
        
        [Column("password", Order = 3)]
        public String password { get; set; }               
        
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

        [Column("token", Order = 8)]
        public String token { get; set; }

        //Relationship table
        public List<Order> Orders { get; set; }
        public Role Role { get; set; }

        //Contructor
        public Account(Guid accountId, string userName, string password, string phoneNumber, string email, string name, string token)
        {
            this.accountId = accountId;
            this.userName = userName;
            this.password = password;            
            this.phoneNumber = phoneNumber;
            this.email = email;
            this.name = name;
            this.token = token;
        }        
    }
}
