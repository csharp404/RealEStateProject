namespace Presentation.Models
{
    public class Comments
    {
        public string Id { get; set; }
        public string Description { get; set; }

        
        public string RealESID { get; set; }
        public RealES RealES { get; set; }

       
        public string UserID { get; set; }
        public User User { get; set; }
    }
}

