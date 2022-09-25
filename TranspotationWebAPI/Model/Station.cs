using System.ComponentModel.DataAnnotations.Schema;
using TranspotationWebAPI.Model.Abstract;

namespace TranspotationWebAPI.Model
{
    [Table("Station", Schema ="dbo")]
    public class Station : Common
    {
        [Column("Name")]
        public string Name { get; set; }

        [Column("Address")]
        public string Address { get; set; }

        [Column("LocationId")]
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public Station(string Name, string Address, int LocationId)
        {
            this.Name = Name;
            this.Address = Address;
            this.LocationId = LocationId;            
        }
    }
}
