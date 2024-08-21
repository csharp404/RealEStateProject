namespace Presentation.Models
{
    public class Feature
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public ICollection<RealESFeature> RealESFeatures { get; set; }
    }
}
