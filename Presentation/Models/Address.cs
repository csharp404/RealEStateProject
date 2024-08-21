namespace Presentation.Models
{
    public class Address
    {
        public string AddressID { get; set; } = Guid.NewGuid().ToString();

        public string CountryID { get; set; }
        public Country Country { get; set; }

        public string CityID { get; set; }
        public City City { get; set; }

        public string HoodID { get; set; }
        public Hood Hood { get; set; }

        //RealEstate
        public string RealESID { get; set; }
        public RealES RealES { get; set; }
    }
}
