namespace Presentation.Models
{
    public class Services
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public ICollection<RealESService> RealESServices { get; set; }
    }
}
