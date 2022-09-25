using System.ComponentModel.DataAnnotations.Schema;
using TranspotationWebAPI.Model.Abstract;

namespace TranspotationWebAPI.Model
{
    [Table("Car", Schema = "dbo")]
    public class Car : Common
    {
        [Column("Name")]
        public string Name { get; set; }   
        public Car (string Name)
        {
            this.Name = Name;            
        }
    }
}
