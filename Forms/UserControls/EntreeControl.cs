using Diner.CustomEventArgs;
using Diner.Models;

namespace Diner
{
    public partial class EntreeControl : UserControl
    {
        public event EventHandler<EntreeControlEventArgs> AddToCartClicked;
        public event EventHandler<EntreeControlEventArgs> EditEntreeClicked;
        public event EventHandler<EntreeControlEventArgs> EditSauceClicked;
        public Entree Entree { get; set; }
        public EntreeControl(Entree entree)
        {
            Entree = entree;
            InitializeComponent();
            WireAllControls(this);

            picIcon.Image = entree.Image;
            lblName.Text = entree.Name;
            lblPrice.Text = $"₱ {entree.Price}";
        }

        private void WireAllControls(Control control)
        {
            foreach (Control ctrl in control.Controls)
            {
                ctrl.Click += Control_Click!;
                if (ctrl.HasChildren)
                {
                    WireAllControls(ctrl);
                }
            }
        }

        private void Control_Click(object sender, EventArgs e)
        {
            InvokeOnClick(this, EventArgs.Empty);

            AddToCartClicked?.Invoke(this,
                new EntreeControlEventArgs(this));

            switch (Entree.Type)
            {
                case Models.Type.ENTREE:
                    EditEntreeClicked?.Invoke(this,
                        new EntreeControlEventArgs(this));
                    break;
                case Models.Type.SAUCE:
                    EditSauceClicked?.Invoke(this,
                       new EntreeControlEventArgs(this));
                    break;
            }


        }
    }
}