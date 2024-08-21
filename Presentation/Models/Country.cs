namespace Presentation.Models
{
    public class Country
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public ICollection<Address> Addresses { get; set; }
        public ICollection<City> Cities{ get; set; }

    }
}
