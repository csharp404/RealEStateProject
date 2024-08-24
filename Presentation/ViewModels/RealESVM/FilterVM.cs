namespace Presentation.ViewModels.RealESVM
{
    public class FilterVM
    {
        public int MaxPrice { get; set; }
        public int MinPrice { get; set; }
        public List<SelectionFeatures>? Categories { get; set; }
        public List<SelectionFeatures>? Feature { get; set; }
        public string CountryFilter { get; set; }
        public string CityFilter { get; set; }
        public string HoodFilter { get; set; }
    }
}
