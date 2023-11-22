using Diner.Models;

namespace Diner.UserControls
{
    public partial class CartItem : UserControl
    {
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
            lblPrice.Text = $"₱ {Entree.Price * Entree.Quantity}";
            picIcon.Image = Entree.Image;
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}