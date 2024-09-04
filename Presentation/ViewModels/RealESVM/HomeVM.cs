using System.Web.Mvc;

namespace Presentation.ViewModels.RealESVM
{
    public sealed record class HomeVM
    {
        public List<CardVM>? Cards { set; get; } 
        public List<CountCategoty>? CountsCategory { set; get; }
        public string? Word { set; get; }
        public string? CategoryId { set; get; }    
        public string? PlaceCountry { set; get; }    
        public string? Price { set; get; }   
    }
}
