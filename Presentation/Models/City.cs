namespace Presentation.Models
{
    public class City
    {
        public string ID { get; set; }
        public string Name { get; set; }

        public ICollection<Address> Addresses { get; set; }

        public Country Country { get; set; }
        public string CountryId { get; set; }
        public ICollection<Hood> hoods { get; set; }
    }
}
