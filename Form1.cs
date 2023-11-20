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
            _entreeControls.Add(
                new EntreeControl(
                    new Entree
                    {
                        Id = 1,
                        Image = Image.FromFile("./Icons/burger.jpg"),
                        Name = "Burger",
                        Price = 25
                    }));

            foreach (var entree in _entreeControls)
            {
                entree.AddToCartClicked += Entree_AddToCartClicked!;
                panelItem.Controls.Add(entree);
            }
        }

        private void Entree_AddToCartClicked(object sender, EntreeEventArgs e)
        {
            panelCart.Controls.Add(
                new CartItem(e.EntreeControl.Entree));
        }

        private void lbSauce_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbSauce.SelectedItem == null)
            {
                return;
            }

            panelCart.Controls.Add(
                new CartItem(
                    new Entree()
                    {
                        Id = 10,
                        Image = null,
                        Name = lbSauce.SelectedItem.ToString(),
                        Price = 0,
                        Quantity = 0
                    }));
        }
    }
}