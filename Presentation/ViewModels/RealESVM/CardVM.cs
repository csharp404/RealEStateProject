using System.Web.Mvc;

namespace Presentation.ViewModels.RealESVM
{
    public class CardVM
    {
        public string RealId { get; set; }
        public List<string> ImageName { get; set; }
        public string? Title { get; set; }
        public int Price { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Hood { get; set; }
        public string N_BedRoom { get; set; }
        public string TotalRooms { get; set; }
        public int Area_Siza { get; set; }
        public string UserName { get; set; }
        public string UserPP { get; set; }
        public string UserID{ get; set; }
        public string Date { get; set; }
       public List<SelectionFeatures> Categories { get; set; }
        public List<SelectionFeatures> Features { get; set; } 
        public bool isFav { get; set; } = false;
    }
}
