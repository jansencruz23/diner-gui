using Guna.UI2.WinForms;

namespace Diner.Forms
{
    public partial class AdminForm : Form
    {
        private bool _entreeExpand;
        private bool _drinksExpand;

        public AdminForm()
        {
            InitializeComponent();
        }

        private void btnEntree_Click(object sender, EventArgs e)
        {
            timerEntree.Start();
        }

        private void timerEntree_Tick(object sender, EventArgs e)
        {
            ExpandPanel(panelEntree, ref _entreeExpand, timerEntree);
        }

        private void timerDrinks_Tick(object sender, EventArgs e)
        {
            ExpandPanel(panelDrinks, ref _drinksExpand, timerDrinks);
        }

        private void ExpandPanel(Guna2Panel panel, ref bool expanded,
            System.Windows.Forms.Timer timer)
        {
            var change = expanded ? 10 : -10;
            panel.Height += change;

            if ((expanded && panel.Height >= panel.MaximumSize.Height)
                || (!expanded && panel.Height <= panel.MinimumSize.Height))
            {
                expanded = !expanded;
                timer.Stop();
            }
        }

        private void btnDrinks_Click(object sender, EventArgs e)
        {
            timerDrinks.Start();
        }
    }
}