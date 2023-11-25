using Guna.UI2.WinForms;

namespace Diner.Forms
{
    public partial class AdminForm : Form
    {
        private bool _menuExpand;
        private bool _entreeExpand;
        private bool _drinksExpand;
        private bool _sauceExpand;
        private bool _requestExpand;

        public AdminForm()
        {
            InitializeComponent();
        }

        private void btnEntree_Click(object sender, EventArgs e)
        {
            timerEntrees.Start();
            CheckMenuExpand();
            ColorButton(btnEntree);
            CloseTabs(ref _drinksExpand, timerDrinks);
            CloseTabs(ref _sauceExpand, timerSauces);
            CloseTabs(ref _requestExpand, timerRequests);
        }

        private void btnDrinks_Click(object sender, EventArgs e)
        {
            timerDrinks.Start();
            CheckMenuExpand();
            ColorButton(btnDrinks);
            CloseTabs(ref _entreeExpand, timerEntrees);
            CloseTabs(ref _sauceExpand, timerSauces);
            CloseTabs(ref _requestExpand, timerRequests);
        }

        private void btnSauce_Click(object sender, EventArgs e)
        {
            timerSauces.Start();
            CheckMenuExpand();
            ColorButton(btnSauce);
            CloseTabs(ref _entreeExpand, timerEntrees);
            CloseTabs(ref _drinksExpand, timerDrinks);
            CloseTabs(ref _requestExpand, timerRequests);
        }

        private void btnRequest_Click(object sender, EventArgs e)
        {
            timerRequests.Start();
            CheckMenuExpand();
            ColorButton(btnRequest);
            CloseTabs(ref _entreeExpand, timerEntrees);
            CloseTabs(ref _drinksExpand, timerDrinks);
            CloseTabs(ref _sauceExpand, timerSauces);
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            timerMenu.Start();
        }

        private void CloseTabs(ref bool expand, System.Windows.Forms.Timer timer)
        {
            if (!expand)
            {
                timer.Start();
            }
        }

        private void CheckMenuExpand()
        {
            if (_menuExpand)
            {
                timerMenu.Start();
            }
        }

        private void timerMenu_Tick(object sender, EventArgs e)
        {
            ExpandMenu();
        }

        private void timerSauces_Tick(object sender, EventArgs e)
        {
            ExpandPanel(panelSauce, ref _sauceExpand, timerSauces);
        }

        private void timerRequests_Tick(object sender, EventArgs e)
        {
            ExpandPanel(panelRequest, ref _requestExpand, timerRequests);
        }

        private void timerEntree_Tick(object sender, EventArgs e)
        {
            ExpandPanel(panelEntree, ref _entreeExpand, timerEntrees);
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

        private void ExpandMenu()
        {
            var change = _menuExpand ? 10 : -10;
            flowPanelMenu.Width += change;

            if ((_menuExpand && flowPanelMenu.Width >= flowPanelMenu.MaximumSize.Width)
                || (!_menuExpand && flowPanelMenu.Width <= flowPanelMenu.MinimumSize.Width))
            {
                _menuExpand = !_menuExpand;
                timerMenu.Stop();
            }
        }

        private void ColorButton(Guna2Button button)
        {
            var selectedColor = Color.FromArgb(32, 32, 42);
            var defaultColor = Color.FromArgb(23, 23, 31);

            btnEntree.FillColor = defaultColor;
            btnDrinks.FillColor = defaultColor;
            btnSauce.FillColor = defaultColor;
            btnRequest.FillColor = defaultColor;

            button.FillColor = selectedColor;
        }
    }
}