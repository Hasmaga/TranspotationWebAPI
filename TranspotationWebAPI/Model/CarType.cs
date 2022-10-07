using System.ComponentModel.DataAnnotations.Schema;
using TranspotationWebAPI.Model.Abstract;

namespace TranspotationWebAPI.Model
{
    [Table("CarType", Schema = "dbo")]
    public class CarType : Common
    {
        [Column("TypeCar")]
        public string TypeCar { get; set; }

        [Column("Name")]
        public string? Name { get; set; }

        [Column("Image")]
        public string? Image { get; set; }

        public CarType (string TypeCar, string? Name, string? Image)
        {
            this.TypeCar = TypeCar;
            this.Name = Name;
            this.Image = Image;
        }
    }
}
