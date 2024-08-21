using Microsoft.AspNetCore.Identity;

namespace Presentation.Models
{
    public class User : IdentityUser
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImagePath { get; set; }
        public string ImageName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public ICollection<Comments> Comments { get; set; }
        public ICollection<Favorite> Favorites { get; set; }
        public ICollection<RealES> RealES { get; set; }
    }
}
