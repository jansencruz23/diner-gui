using Diner.CustomEventArgs;
using Diner.Models;
using Diner.UserControls;
using Guna.UI2.WinForms;

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
            #region --INITIALIZE ENTREE CONTROLS--
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

            #endregion

            _entreeControls.AddRange(entrees);

            foreach (var entree in _entreeControls)
            {
                entree.AddToCartClicked += Entree_AddToCartClicked!;
                panelItem.Controls.Add(entree);
            }
        }

        private void Entree_AddToCartClicked(object sender, EntreeEventArgs e)
        {
            var existingItem = GetExistingCartItem(e.EntreeControl.Entree.Id);

            if (existingItem != null)
            {
                existingItem.Entree.Quantity++;
                existingItem.InitializeCart();
                return;
            }

            var cartItem = new CartItem(e.EntreeControl.Entree);
            cartItem.RemoveFromCartClicked += RemoveFromCart_Clicked!;
            panelCart.Controls.Add(cartItem);
        }

        private void RemoveFromCart_Clicked(object sender, CartItemEventArgs e)
        {
            panelCart.Controls.Remove(e.CartItem);
            e.CartItem.Entree.Quantity = 1;
        }

        private void lbSauce_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbSauce.SelectedItem == null)
            {
                return;
            }

            foreach (var item in lbSauce.SelectedItems)
            {
                var itemName = item.ToString();
                UpdateCartItem(itemName);
            }
        }

        private void UpdateCartItem(string itemName)
        {
            var existingItem = GetExistingCartItem(itemName);

            if (existingItem != null)
            {
                existingItem.Entree.Quantity++;
                existingItem.InitializeCart();
                return;
            }

            AddNewCartItem(itemName);
        }

        private void AddNewCartItem(string itemName)
        {
            var cartItem = new CartItem(
                    new Entree
                    {
                        Name = itemName,
                        Image = Image.FromFile($"./Icons/{itemName}.jpg"),
                        Price = 0,
                        Quantity = 1
                    });

            cartItem.RemoveFromCartClicked += RemoveFromCart_Clicked!;
            panelCart.Controls.Add(cartItem);
        }

        private CartItem GetExistingCartItem(string itemName)
            => panelCart.Controls
                .OfType<CartItem>()
                .FirstOrDefault(cart => cart.Entree.Name.Equals(itemName))!;

        private CartItem GetExistingCartItem(int id)
            => panelCart.Controls
                .OfType<CartItem>()
                .FirstOrDefault(cart => cart.Entree.Id == id)!;

        private void cbRequest_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbRequest.SelectedItem == null)
            {
                return;
            }

            var itemName = cbRequest.SelectedItem.ToString();

            var entree = new Entree
            {
                Id = 99,
                Name = itemName,
                Image = Image.FromFile($"./Icons/{itemName}.jpg"),
                Price = 0,
                Quantity = 1
            };

            AddOrUpdateSingleItem(entree);
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            var itemName = (sender as Guna2RadioButton).Text;

            if (drinkDetails.TryGetValue(itemName, out var drinkInfo))
            {
                var itemImage = Image.FromFile($"./Icons/{drinkInfo.imageName}");
                AddOrUpdateSingleItem(new Entree
                {
                    Id = 98,
                    Name = itemName,
                    Image = itemImage,
                    Price = drinkInfo.price,
                    Quantity = 1
                });
            }
        }

        private void AddOrUpdateSingleItem(Entree entree)
        {
            var existingItem = panelCart.Controls
                .OfType<CartItem>()
                .FirstOrDefault(cart => cart.Entree.Id == entree.Id);

            if (existingItem != null)
            {
                panelCart.Controls.Remove(existingItem);
            }

            var cartItem = new CartItem(entree);
            cartItem.RemoveFromCartClicked += RemoveFromCart_Clicked!;

            panelCart.Controls.Add(cartItem);
        }

        private readonly Dictionary<string, (string imageName, int price)> drinkDetails = new Dictionary<string, (string, int)>
        {
            { "Milk", ("Milk.jpg", 15) },
            { "Juice", ("Juice.jpg", 15) },
            { "Soda", ("Soda.jpg", 25) },
            { "Lemonade", ("Lemonade.jpg", 30) },
            { "Tea", ("Tea.jpg", 30) },
            { "Coffee", ("Coffee.jpg", 35) }
        };
    }
}