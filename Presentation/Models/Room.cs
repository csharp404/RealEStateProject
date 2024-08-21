namespace Presentation.Models
{
    public class Room
    {
        public string RoomID { get; set; }

        public string N_LivingRoom { get; set; }
        public string N_Bathroom { get; set; }
        public string N_Garage { get; set; }
        public string N_Bedroom { get; set; }
        public string N_Kitchen { get; set; }
        public string N_Floors { get; set; }
        public string N_Rooms { get; set; }

        public string RealESId { get; set; }
        public RealES RealES { get; set; }
    }
}
