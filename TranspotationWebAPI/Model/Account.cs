﻿using System.ComponentModel.DataAnnotations;
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
        
        [Column("roleId", Order = 4)]
        public int roleId { get; set; }        
        
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
        
        public Account(int accountId, string userName, string password, int roleId, string phoneNumber, string email, string name, string token)
        {
            this.accountId = accountId;
            this.userName = userName;
            this.password = password;
            this.roleId = roleId;
            this.phoneNumber = phoneNumber;
            this.email = email;
            this.name = name;
            this.token = token;
        }
        public List<Order> orders { get; set; }
        public Role Role { get; set; }
    }
}
