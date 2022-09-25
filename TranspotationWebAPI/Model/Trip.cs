using System.ComponentModel.DataAnnotations.Schema;
using TranspotationWebAPI.Model.Abstract;

namespace TranspotationWebAPI.Model
{
    [Table("Trip", Schema = "dbo")]
    public class Trip : Common
    {        
        public int? From_Id { get; set; } 
        public int? To_Id { get; set; }       
        public Location From { get; set; }               
        public Location To { get; set; }

        public Trip(int? From_Id, int? To_Id)
        {
            this.From_Id = From_Id;
            this.To_Id = To_Id;
        }
    }
}
