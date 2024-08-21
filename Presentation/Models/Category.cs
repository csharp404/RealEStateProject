namespace Presentation.Models
{
    public class Category
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<RealES> RealEs { get; set; }
    }
}
