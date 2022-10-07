using System.ComponentModel.DataAnnotations.Schema;
using TranspotationWebAPI.Model.Abstract;

namespace TranspotationWebAPI.Model
{
    [Table("Location", Schema = "dbo")]
    public class Location : Common
    {
        [Column("Name")]
        public string Name { get; set; }

        public ICollection<Trip> FromTrip { get; set; } = new List<Trip>();
        public ICollection<Trip> ToTrip { get; set; } = new List<Trip>();

        public Location(string Name)
        {
            this.Name = Name;
        }
    }
}
