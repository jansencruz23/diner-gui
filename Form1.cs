using Diner.CustomEventArgs;
using Diner.Models;

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
            MessageBox.Show(e.EntreeControl.Entree.Name);
        }
    }
}