using Diner.Models;

namespace Diner.Helpers
{
    public class EntreeSeeder
    {
        private List<Entree> _entrees;

        public EntreeSeeder(List<Entree> entrees)
        {
            _entrees = entrees;
        }

        public void SeedEntrees()
        {
            if (_entrees.Count <= 0)
            {
                _entrees.AddRange(DefaultEntrees());
            }
        }

        private IEnumerable<Entree> DefaultEntrees()
        {
            yield return CreateEntree(1, "./Icons/burger.jpg", "Burger", 35);
            yield return CreateEntree(2, "./Icons/Fried Chicken.jpg", "Fried Chicken", 75);
            yield return CreateEntree(3, "./Icons/Chicken Masala.jpg", "Chicken Masala", 140);
            yield return CreateEntree(4, "./Icons/Chicken Biryani.jpg", "Chicken Biryani", 120);
            yield return CreateEntree(5, "./Icons/Sisig.jpg", "Sisig", 100);
            yield return CreateEntree(6, "./Icons/Chicken Teriyaki.jpg", "Chicken Teriyaki", 90);
            yield return CreateEntree(7, "./Icons/Reuben.jpg", "Reuben", 60);
            yield return CreateEntree(8, "./Icons/French Fries.jpg", "French Fries", 40);
            yield return CreateEntree(9, "./Icons/Pizza.jpg", "Pizza", 230);
            yield return CreateEntree(10, "./Icons/Chopsuey.jpg", "Chopsuey", 90);
        }

        private Entree CreateEntree(int id, string imagePath, string name, int price)
        {
            return new Entree
            {
                Id = id,
                Image = Image.FromFile(imagePath),
                Name = name,
                Price = price,
                Quantity = 1,
                Type = Models.Type.ENTREE
            };
        }
    }
}