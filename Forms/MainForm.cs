using Diner.CustomEventArgs;
using Diner.Forms;
using Diner.Models;
using Diner.UserControls;
using Guna.UI2.WinForms;

namespace Diner
{
    public partial class MainForm : Form
    {
        private List<EntreeControl> _entreeControls;
        private List<Entree> _entrees;
        private List<Entree> _drinks;
        private double _total;
        private bool _extraPanelExpand;

        public MainForm()
        {
            _entreeControls = new();
            _entrees = new();
            InitializeComponent();
            InitializeEntrees();
        }

        public MainForm(List<Entree> entree)
        {
            _entreeControls = new();
            _entrees = entree;
            InitializeComponent();
            InitializeEntrees();
        }

        private void InitializeEntrees()
        {
            if (_entrees.Count <= 0)
            {
                #region --INITIALIZE ENTREE CONTROLS--
                _entreeControls = new List<EntreeControl>
            {
                new EntreeControl(
                    new Entree
                    {
                        Id = 1,
                        Image = Image.FromFile("./Icons/burger.jpg"),
                        Name = "Burger",
                        Price = 35,
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
                        Image = Image.FromFile("./Icons/Chicken Masala.jpg"),
                        Name = "Chicken Masala",
                        Price = 140,
                        Quantity = 1
                    }),
                new EntreeControl(
                    new Entree
                    {
                        Id = 4,
                        Image = Image.FromFile("./Icons/Chicken Biryani.jpg"),
                        Name = "Chicken Biryani",
                        Price = 120,
                        Quantity = 1
                    }),
                new EntreeControl(
                    new Entree
                    {
                        Id = 5,
                        Image = Image.FromFile("./Icons/Sisig.jpg"),
                        Name = "Sisig",
                        Price = 100,
                        Quantity = 1
                    }),
                new EntreeControl(
                    new Entree
                    {
                        Id = 6,
                        Image = Image.FromFile("./Icons/Chicken Teriyaki.jpg"),
                        Name = "Chicken Teriyaki",
                        Price = 90,
                        Quantity = 1
                    }),
                new EntreeControl(
                    new Entree
                    {
                        Id = 7,
                        Image = Image.FromFile("./Icons/Reuben.jpg"),
                        Name = "Reuben",
                        Price = 60,
                        Quantity = 1
                    }),
                new EntreeControl(
                    new Entree
                    {
                        Id = 8,
                        Image = Image.FromFile("./Icons/French Fries.jpg"),
                        Name = "French Fries",
                        Price = 40,
                        Quantity = 1
                    }),
                new EntreeControl(
                    new Entree
                    {
                        Id = 9,
                        Image = Image.FromFile("./Icons/Pizza.jpg"),
                        Name = "Pizza",
                        Price = 230,
                        Quantity = 1
                    }),
                new EntreeControl(
                    new Entree
                    {
                        Id = 10,
                        Image = Image.FromFile("./Icons/Chopsuey.jpg"),
                        Name = "Chopsuey",
                        Price = 90,
                        Quantity = 1
                    }),
            };

                #endregion
            }
            else
            {
                foreach (var entree in _entrees)
                {
                    _entreeControls.Add(new EntreeControl(entree));
                }
            }

            foreach (var entree in _entreeControls)
            {
                entree.AddToCartClicked += Entree_AddToCartClicked!;
                panelItem.Controls.Add(entree);
                _entrees.Add(entree.Entree);
            }
        }

        private void Entree_AddToCartClicked(object sender, EntreeControlEventArgs e)
        {
            var existingItem = GetExistingCartItem(e.EntreeControl.Entree.Id);

            if (existingItem != null)
            {
                existingItem.Entree.Quantity++;
                existingItem.InitializeCart();
                RefreshTotal();
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
                Price = 10,
                Quantity = 1
            };

            AddOrRemoveSingleItem(entree);
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            var itemName = (sender as Guna2RadioButton).Text;

            if (drinkDetails.TryGetValue(itemName, out var drinkInfo))
            {
                var itemImage = Image.FromFile($"./Icons/{drinkInfo.imageName}");
                AddOrRemoveSingleItem(new Entree
                {
                    Id = 98,
                    Name = itemName,
                    Image = itemImage,
                    Price = drinkInfo.price,
                    Quantity = 1
                });
            }
        }

        private void AddOrRemoveSingleItem(Entree entree)
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

        private void panelCart_ControlAdded(object sender, ControlEventArgs e)
        {
            RefreshTotal();
        }

        private void RefreshTotal()
        {
            _total = 0;
            foreach (var cartItem in panelCart.Controls.OfType<CartItem>())
            {
                var itemPrice = cartItem.Entree.Quantity * cartItem.Entree.Price;
                _total += itemPrice;
                lblPrice.Text = $"₱ {_total}";
            }
        }

        private void panelCart_ControlRemoved(object sender, ControlEventArgs e)
        {
            if (panelCart.Controls.Count <= 0)
            {
                _total = 0;
                lblPrice.Text = $"₱ {_total}";
                return;
            }

            RefreshTotal();
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            var entrees = new List<Entree>();
            foreach(var cart in panelCart.Controls.OfType<CartItem>())
            {
                entrees.Add(cart.Entree);
            }

            var receiptForm = new ReceiptForm(entrees);
            receiptForm.ShowDialog();
            ClearCart();
        }

        private void cbWater_CheckedChanged(object sender, EventArgs e)
        {
            if (cbWater.Checked)
            {
                AddNewCartItem("Water");
                return;
            }

            var cartWater = panelCart.Controls.OfType<CartItem>()
                .FirstOrDefault(i => i.Entree.Name.Equals("Water"));

            panelCart.Controls.Remove(cartWater);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearCart();
        }

        private void ClearCart()
        {
            foreach (var item in panelCart.Controls.OfType<CartItem>())
            {
                item.Entree.Quantity = 1;
            }
            panelCart.Controls.Clear();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            timerPanel.Start();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageDialog.Show(this, "Diner GUI Developed by: \n" +
                "Cruz, Jansen\n" +
                "Dizon, Aris Justine\n" +
                "Lloren, Alberto\n" +
                "Munoz, Nathan Sheary",
                "Diner GUI OOP10",
                MessageDialogButtons.OK,
                MessageDialogIcon.Information,
                MessageDialogStyle.Dark);
        }

        private void timerPanel_Tick(object sender, EventArgs e)
        {
            var change = _extraPanelExpand ? 10 : -10;
            panelExtra.Height += change;

            if ((_extraPanelExpand && panelExtra.Height >= panelExtra.MaximumSize.Height)
                || (!_extraPanelExpand && panelExtra.Height <= panelExtra.MinimumSize.Height))
            {
                _extraPanelExpand = !_extraPanelExpand;
                timerPanel.Stop();
            }
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var adminForm = new AdminForm(_entrees);
            adminForm.ShowDialog();
            Hide();
        }
    }
}