using Diner.CustomEventArgs;
using Diner.Models;

namespace Diner.UserControls
{
    public partial class CartItem : UserControl
    {
        public event EventHandler<CartItemEventArgs> RemoveFromCartClicked;
        public Entree Entree { get; set; }
        public CartItem(Entree entree)
        {
            Entree = entree;
            InitializeComponent();
            InitializeCart();
        }

        public void InitializeCart()
        {
            lblName.Text = Entree.Name;
            lblQuantity.Text = $"x{Entree.Quantity}";
            lblPrice.Text = Entree.Price <= 0 
                ? "Free" 
                : $"₱ {Entree.Price * Entree.Quantity}";
            picIcon.Image = Entree.Image;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            RemoveFromCartClicked?.Invoke(this,
                new CartItemEventArgs(this));
        }
    }
}