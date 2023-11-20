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

            lblName.Text = entree.Name;
            lblPrice.Text = $"₱ {entree.Price}";
            lblQuantity.Text = $"x{entree.Quantity}";
            picIcon.Image = entree.Image;
        }
    }
}