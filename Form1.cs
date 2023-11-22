using Diner.CustomEventArgs;
using Diner.Models;
using Diner.UserControls;

namespace Diner
{
    public partial class Form1 : Form
    {
        private List<EntreeControl> _entreeControls;
        public Form1()
        {
            _entreeControls = new();
            InitializeComponent();
            InitializeEntrees();
        }

        private void InitializeEntrees()
        {
            var entrees = new List<EntreeControl>
            {
                new EntreeControl(
                    new Entree
                    {
                        Id = 1,
                        Image = Image.FromFile("./Icons/burger.jpg"),
                        Name = "Burger",
                        Price = 25,
                        Quantity = 1
                    }),
                new EntreeControl(
                    new Entree
                    {
                        Id = 2,
                        Image = Image.FromFile("./Icons/Fried Chicken.jpg"),
                        Name = "Fried Chicken",
                        Price = 75,
                        Quantity = 1
                    }),
                new EntreeControl(
                    new Entree
                    {
                        Id = 3,
                        Image = Image.FromFile("./Icons/Fried Chicken.jpg"),
                        Name = "Fried Chicken",
                        Price = 75,
                        Quantity = 1
                    })
            };

            _entreeControls.AddRange(entrees);

            foreach (var entree in _entreeControls)
            {
                entree.AddToCartClicked += Entree_AddToCartClicked!;
                panelItem.Controls.Add(entree);
            }
        }

        private void Entree_AddToCartClicked(object sender, EntreeEventArgs e)
        {
            var existingItem = panelCart.Controls
                .OfType<CartItem>()
                .FirstOrDefault(cart => cart.Entree.Id == e.EntreeControl.Entree.Id);

            if (existingItem != null)
            {
                existingItem.Entree.Quantity++;
                existingItem.InitializeCart();
                return;
            }

            panelCart.Controls.Add(
                new CartItem(e.EntreeControl.Entree));
        }

        private void lbSauce_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbSauce.SelectedItem == null)
            {
                return;
            }

            var items = lbSauce.SelectedItems;

            foreach (var item in items)
            {
                var itemName = item.ToString();
                var existingItem = panelCart.Controls
                    .OfType<CartItem>()
                    .FirstOrDefault(cart => cart.Entree.Name.Equals(itemName));

                if (existingItem != null)
                {
                    existingItem.Entree.Quantity++;
                    existingItem.InitializeCart();
                    break;
                }

                panelCart.Controls.Add(
                    new CartItem(
                        new Entree()
                        {
                            Name = itemName,
                            Image = Image.FromFile($"./Icons/{itemName}.jpg"),
                            Price = 0,
                            Quantity = 1
                        }));
            }


        }

        private void cbRequest_SelectedIndexChanged(object sender, EventArgs e)
        {
            var itemName = cbRequest.SelectedItem.ToString();

            var existingItem = panelCart.Controls
                .OfType<CartItem>()
                .FirstOrDefault(cart => cart.Entree.Id == 99);

            if (existingItem != null)
            {
                panelCart.Controls.Remove(existingItem);
            }

            panelCart.Controls.Add(
                new CartItem(
                    new Entree()
                    {
                        Id = 99,
                        Name = itemName,
                        Image = Image.FromFile($"./Icons/{itemName}.jpg"),
                        Price = 0,
                        Quantity = 1
                    }));
        }
    }
}