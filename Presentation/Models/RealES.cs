using System.ComponentModel.DataAnnotations.Schema;

namespace Presentation.Models
{
    public class RealES
    {
        public string ID { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Area_Size { get; set; }

        public string PhoneNumber { get; set; }
        public string Email { get; set; }

  
        public string UserID { get; set; }
        public User User { get; set; }

        [ForeignKey("AddressID")]
        public string AddressID { get; set; }
        public Address Address { get; set; }

        public ICollection<RealESFeature>? RealESFeatures { get; set; }
        public ICollection<RealESService>? RealESServices { get; set; }
        public ICollection<Favorite>? Favorites { get; set; }
        public ICollection<Comments>? Comments { get; set; }
        public ICollection<RealESImages>? Images{ get; set; }
        public int Views {  get; set; } =0;
        public string RoomID { get; set; }
        public Room Room { get; set; }

        public string CategoryID { get; set; }
        public Category Category { get; set; }

      public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
