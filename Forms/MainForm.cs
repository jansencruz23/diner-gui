using Diner.CustomEventArgs;
using Diner.Forms;
using Diner.Helpers;
using Diner.Models;
using Diner.UserControls;
using Guna.UI2.WinForms;

namespace Diner
{
    public partial class MainForm : Form
    {
        private List<EntreeControl> _entreeControls;
        private List<Entree> _entrees;
        private double _total;
        private bool _extraPanelExpand;

        public MainForm()
        {
            _entreeControls = new();
            _entrees = new();
            InitializeComponent();
            InitializeEntrees();
            InitializeSauce();
            InitializeRequest();
            InitializeDrinks();
        }

        public MainForm(List<Entree> entree)
        {
            _entreeControls = new();
            _entrees = entree;
            InitializeComponent();
            InitializeEntrees();
            InitializeSauce(true);
            InitializeRequest(true);
            InitializeDrinks(true);
        }

        private void InitializeEntrees()
        {
            if (_entrees.Count <= 0)
            {
                var seeder = new EntreeSeeder(_entrees);
                seeder.SeedEntrees();
            }

            _entrees.ForEach(e => _entreeControls.Add(new EntreeControl(e)));

            foreach (var entree in _entreeControls
                .Where(e => e.Entree.Type == Models.Type.ENTREE))
            {
                entree.AddToCartClicked += Entree_AddToCartClicked;
                panelItem.Controls.Add(entree);
            }
        }

        private void InitializeSauce(bool fromAdmin = false)
        {
            if (!fromAdmin)
            {
                _entrees.AddRange(lbSauce.Items.Cast<object>()
                    .Select(sauce => new Entree
                    {
                        Name = sauce.ToString(),
                        Image = Image.FromFile($"./Icons/{sauce}.jpg"),
                        Price = 0,
                        Quantity = 1,
                        Type = Models.Type.SAUCE
                    }));
                return; 
            }

            lbSauce.Items.Clear();
            _entrees.Where(e => e.Type == Models.Type.SAUCE)
                .Select(sauce => sauce.Name)
                .ToList()
                .ForEach(sauce => lbSauce.Items.Add(sauce));
        }

        private void InitializeRequest(bool fromAdmin = false)
        {
            if (!fromAdmin)
            {
                _entrees.AddRange(cbRequest.Items.Cast<object>()
                    .Select(request => new Entree
                    {
                        Id = 99,
                        Name = request.ToString(),
                        Image = Image.FromFile($"./Icons/{request}.jpg"),
                        Price = 10,
                        Quantity = 1,
                        Type = Models.Type.REQUEST
                    })
                );
                return;
            }

            cbRequest.Items.Clear();
            _entrees.Where(e => e.Type == Models.Type.REQUEST)
                .Select(request => request.Name)
                .ToList()
                .ForEach(request => cbRequest.Items.Add(request));
        }

        private void InitializeDrinks(bool fromAdmin = false)
        {
            if (!fromAdmin)
            {
                AddDrinksFromDetails();
                return;
            }

            CreateRadioButtonsForDrinks();
        }

        private void AddDrinksFromDetails()
        {
            _entrees.AddRange(drinkDetails
                .Where(pair => pair.Value != default)
                .Select(pair =>
                {
                    var drinkInfo = pair.Value;
                    var itemImage = Image.FromFile($"./Icons/{drinkInfo.imageName}");

                    return new Entree
                    {
                        Id = 98,
                        Name = pair.Key,
                        Image = itemImage,
                        Price = drinkInfo.price,
                        Quantity = 1,
                        Type = Models.Type.DRINK
                    };
                }));
        }

        private void CreateRadioButtonsForDrinks()
        {
            tableDrink.Controls.Clear();
            _entrees.Where(e => e.Type == Models.Type.DRINK)
                .Select(request => new Guna2RadioButton
                {
                    Text = request.Name,
                    AutoSize = true,
                    BackColor = Color.FromArgb(32, 32, 42)
                })
                .ToList()
                .ForEach(radioButton =>
                {
                    SetToolTipAndEventHandler(radioButton);
                    tableDrink.Controls.Add(radioButton);
                });
        }

        private void SetToolTipAndEventHandler(Guna2RadioButton radioButton)
        {
            var drinkPrice = _entrees.First(e => e.Name == radioButton.Text).Price;
            toolTip.SetToolTip(radioButton, $"Price: ₱ {drinkPrice}");
            radioButton.CheckedChanged += new EventHandler(RadioButton_CheckedChanged);
        }

        private void Entree_AddToCartClicked(object? sender, EntreeControlEventArgs e)
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
            if (lbSauce.SelectedItem != null)
            {
                foreach (var item in lbSauce.SelectedItems)
                {
                    var itemName = item.ToString();
                    UpdateCartItem(itemName);
                    RefreshTotal();
                }
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
            var sauce = _entrees.FirstOrDefault(s => s.Type == Models.Type.SAUCE 
                && s.Name.Equals(itemName));

            var cartItem = sauce == null
                ? new CartItem(new Entree
                {
                    Name = itemName,
                    Image = Image.FromFile($"./Icons/{itemName}.jpg"),
                    Price = 0,
                    Quantity = 1,
                    Type = Models.Type.SAUCE
                })
                : new CartItem(sauce);

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
            var request = _entrees.FirstOrDefault(s => s.Type == Models.Type.REQUEST 
                && s.Name.Equals(itemName))
                    ?? new Entree
                    {
                        Id = 99,
                        Name = itemName,
                        Image = Image.FromFile($"./Icons/{itemName}.jpg"),
                        Price = 10,
                        Quantity = 1,
                        Type = Models.Type.REQUEST
                    };

            AddOrRemoveSingleItem(request);
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            var itemName = (sender as Guna2RadioButton).Text;

            var drink = _entrees
                .Where(d => d.Type == Models.Type.DRINK)
                .FirstOrDefault(d => d.Name.Equals(itemName));
            AddOrRemoveSingleItem(drink);
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
            if (panelCart.Controls.Count <= 0)
            {
                MessageDialog.Show(this, "Please order first", "Purchase Failed",
                    MessageDialogButtons.OK, MessageDialogIcon.Error,
                    MessageDialogStyle.Dark);
                return;
            }

            var entrees = new List<Entree>();
            foreach (var cart in panelCart.Controls.OfType<CartItem>())
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
                "Cruz, Jansen\n" + "Dizon, Aris Justine\n" +
                "Lloren, Alberto\n" + "Munoz, Nathan Sheary",
                "Diner GUI OOP10", MessageDialogButtons.OK,
                MessageDialogIcon.Information, MessageDialogStyle.Dark);
        }

        private void timerPanel_Tick(object sender, EventArgs e)
        {
            var change = _extraPanelExpand ? 5 : -5;
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
            var form = new AdminLoginForm(Hide, _entrees);
            form.FormClosed += (s, args) => Close();
            form.ShowDialog();
        }
    }
}