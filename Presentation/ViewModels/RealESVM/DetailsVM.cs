namespace Presentation.ViewModels.RealESVM
{
    public class DetailsVM
    {
        public List<string> ImageName { get; set; }
        public string Description { get; set; } 
        public string Title { get; set; }
        public int Price { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Hood { get; set; }
        public string N_BedRoom { get; set; }
        public string TotalRooms { get; set; }
        public string N_Bathrooms { get; set; }
        public int Area_Siza { get; set; }
        public string UserName { get; set; }
        public string UserPP { get; set; }
        public string Email { get; set; }   
        public string Date { get; set; }
        public string Garage { get; set; }
        public string PhoneNumber { get; set; }
        public List<String> Features { get; set; }
        public string userID {  get; set; }  
        public string RealID {  get; set; }  
       public  ICollection<Comments> Commentslist { get; set; }
        public string Comment { get; set; }
        public int Views { get; set; }
        
    }
}
