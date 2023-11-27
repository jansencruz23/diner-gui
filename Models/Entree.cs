namespace Diner.Models
{
    public class Entree
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public Image Image { get; set; }
        public Type Type { get; set; }
    }
}