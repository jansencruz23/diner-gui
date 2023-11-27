using Diner.CustomEventArgs;
using Diner.Forms.UserControls;
using Diner.Models;
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

        private List<Entree> _entrees;
        private List<EntreeControl> _entreeControls;

        public AdminForm(List<Entree> entrees)
        {
            _entrees = entrees;
            _entreeControls = new();
            InitializeComponent();
            PopulateEntreeControl();
        }

        private void btnEntree_Click(object sender, EventArgs e)
        {
            timerEntrees.Start();
            CheckMenuExpand();
            ColorButton(btnEntree);
            CloseTabs(ref _sauceExpand, timerSauces);
            CloseTabs(ref _requestExpand, timerRequests);

            PopulateEntreeControl();
        }

        private void PopulateEntreeControl()
        {
            _entreeControls.Clear();
            panelBody.Controls.Clear();
            foreach (var entree in _entrees.Where(e => e.Type == Models.Type.ENTREE))
            {
                var entreeControl = new EntreeControl(entree);
                entreeControl.EditEntreeClicked += EntreeControl_EditEntreeClicked;
                _entreeControls.Add(entreeControl);
            }

            var entreeView = new EntreesView(_entreeControls);
            entreeView.Dock = DockStyle.Fill;
            panelBody.Controls.Add(entreeView);
        }

        private void PopulateSauceControl()
        {
            _entreeControls.Clear();
            panelBody.Controls.Clear();
            foreach (var entree in _entrees.Where(e => e.Type == Models.Type.SAUCE))
            {
                var entreeControl = new EntreeControl(entree);
                entreeControl.EditSauceClicked += EntreeControl_EditSauceClicked;
                _entreeControls.Add(entreeControl);
            }

            var entreeView = new EntreesView(_entreeControls);
            entreeView.Dock = DockStyle.Fill;
            panelBody.Controls.Add(entreeView);
        }

        private void PopulateRequestControl()
        {
            _entreeControls.Clear();
            panelBody.Controls.Clear();
            foreach (var entree in _entrees.Where(e => e.Type == Models.Type.REQUEST))
            {
                var entreeControl = new EntreeControl(entree);
                entreeControl.EditRequestClicked += EntreeControl_EditRequestClicked;
                _entreeControls.Add(entreeControl);
            }

            var entreeView = new EntreesView(_entreeControls);
            entreeView.Dock = DockStyle.Fill;
            panelBody.Controls.Add(entreeView);
        }

        private void PopulateDrinkControl()
        {
            _entreeControls.Clear();
            panelBody.Controls.Clear();
            foreach (var entree in _entrees.Where(e => e.Type == Models.Type.DRINK))
            {
                var entreeControl = new EntreeControl(entree);
                entreeControl.EditDrinkClicked += EntreeControl_EditDrinkClicked;
                _entreeControls.Add(entreeControl);
            }

            var entreeView = new EntreesView(_entreeControls);
            entreeView.Dock = DockStyle.Fill;
            panelBody.Controls.Add(entreeView);
        }

        private void EntreeControl_EditEntreeClicked(object? sender, EntreeControlEventArgs e)
        {
            var editForm = new EditForm(e.EntreeControl.Entree);
            editForm.EntreeEdited += EntreeEdited;
            editForm.EntreeDeleted += EntreeDeleted;
            editForm.ShowDialog();
        }

        private void EntreeDeleted(object? sender, EntreeEventArgs e)
        {
            var entree = _entrees.Where(e => e.Type == Models.Type.ENTREE)
                .FirstOrDefault(e => e.Id == e.Id);
            _entrees.Remove(entree);
            PopulateEntreeControl();
        }

        private void SauceDeleted(object? sender, EntreeEventArgs e)
        {
            var entree = _entrees.Where(e => e.Type == Models.Type.SAUCE)
                .FirstOrDefault(e => e.Name == e.Name);
            _entrees.Remove(entree);
            PopulateEntreeControl();
        }

        private void RequestDeleted(object? sender, EntreeEventArgs e)
        {
            var entree = _entrees.Where(e => e.Type == Models.Type.REQUEST)
                .FirstOrDefault(e => e.Name == e.Name);
            _entrees.Remove(entree);
            PopulateEntreeControl();
        }

        private void EntreeControl_EditSauceClicked(object? sender, EntreeControlEventArgs e)
        {
            var editForm = new EditForm(e.EntreeControl.Entree);
            editForm.EntreeEdited += SauceEdited;
            editForm.EntreeDeleted += SauceDeleted;
            editForm.ShowDialog();
        }

        private void EntreeControl_EditRequestClicked(object? sender, EntreeControlEventArgs e)
        {
            var editForm = new EditForm(e.EntreeControl.Entree);
            editForm.EntreeEdited += RequestEdited;
            editForm.EntreeDeleted += RequestDeleted;
            editForm.ShowDialog();
        }

        private void EntreeControl_EditDrinkClicked(object? sender, EntreeControlEventArgs e)
        {
            var editForm = new EditForm(e.EntreeControl.Entree);
            editForm.EntreeEdited += DrinksEdited;
            editForm.ShowDialog();
        }

        private void EntreeEdited(object? sender, EntreeEventArgs e)
        {
            var entree = _entrees
                .Where(i => i.Type == Models.Type.ENTREE)
                .FirstOrDefault(i => e.Entree.Id == i.Id);
            
            if (entree != null)
            {
                _entrees.Remove(entree);
                _entrees.Add(e.Entree);
            }
            else
            {
                _entrees.Add(e.Entree);
                var entreeControl = new EntreeControl(e.Entree);
                entreeControl.EditEntreeClicked += EntreeControl_EditEntreeClicked;
                _entreeControls.Add(entreeControl);
            }

            PopulateEntreeControl();
        }

        private void SauceEdited(object? sender, EntreeEventArgs e)
        {
            var sauce = _entrees
                .Where(i => i.Type == Models.Type.SAUCE)
                .FirstOrDefault(i => e.Entree.Name.Equals(i.Name));

            if (sauce != null)
            {
                _entrees.Remove(sauce);
                _entrees.Add(e.Entree);
            }
            else
            {
                _entrees.Add(e.Entree);
                var entreeControl = new EntreeControl(e.Entree);
                entreeControl.EditEntreeClicked += EntreeControl_EditSauceClicked;
                _entreeControls.Add(entreeControl);
            }

            PopulateSauceControl();
        }

        private void RequestEdited(object? sender, EntreeEventArgs e)
        {
            var request = _entrees
                .Where(i => i.Type == Models.Type.REQUEST)
                .FirstOrDefault(i => e.Entree.Name.Equals(i.Name));

            if (request != null)
            {
                _entrees.Remove(request);
                _entrees.Add(e.Entree);
            }
            else
            {
                _entrees.Add(e.Entree);
                var entreeControl = new EntreeControl(e.Entree);
                entreeControl.EditRequestClicked += EntreeControl_EditRequestClicked;
                _entreeControls.Add(entreeControl);
            }

            PopulateRequestControl();
        }

        private void DrinksEdited(object? sender, EntreeEventArgs e)
        {
            var drink = _entrees
                .Where(i => i.Type == Models.Type.DRINK)
                .FirstOrDefault(i => e.Entree.Name.Equals(i.Name));

            if (drink != null)
            {
                _entrees.Remove(drink);
                _entrees.Add(e.Entree);
            }
            else
            {
                _entrees.Add(e.Entree);
                var entreeControl = new EntreeControl(e.Entree);
                entreeControl.EditDrinkClicked += EntreeControl_EditDrinkClicked;
                _entreeControls.Add(entreeControl);
            }

            PopulateDrinkControl();
        }

        private void btnDrinks_Click(object sender, EventArgs e)
        {
            CheckMenuExpand();
            ColorButton(btnDrinks);
            CloseTabs(ref _entreeExpand, timerEntrees);
            CloseTabs(ref _sauceExpand, timerSauces);
            CloseTabs(ref _requestExpand, timerRequests);
            PopulateDrinkControl();
        }

        private void btnSauce_Click(object sender, EventArgs e)
        {
            timerSauces.Start();
            CheckMenuExpand();
            ColorButton(btnSauce);
            CloseTabs(ref _entreeExpand, timerEntrees);
            CloseTabs(ref _requestExpand, timerRequests);
            PopulateSauceControl();
        }

        private void btnRequest_Click(object sender, EventArgs e)
        {
            timerRequests.Start();
            CheckMenuExpand();
            ColorButton(btnRequest);
            CloseTabs(ref _entreeExpand, timerEntrees);
            CloseTabs(ref _sauceExpand, timerSauces);
            PopulateRequestControl();
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

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Hide();
            var form = new MainForm(_entrees);
            form.FormClosed += (s, args) => Close();
            form.Show();
        }

        private void btnAddEntree_Click(object sender, EventArgs e)
        {
            var form = new EditForm(new Entree
            {
                Id = _entrees.Count + 1,
                Quantity  = 1,
                Type = Models.Type.ENTREE
            });

            form.EntreeEdited += EntreeEdited;
            form.ShowDialog();
            PopulateEntreeControl();
        }

        private void btnAddSauce_Click(object sender, EventArgs e)
        {
            var form = new EditForm(new Entree
            {
                Quantity = 1,
                Type = Models.Type.SAUCE
            });

            form.EntreeEdited += SauceEdited;
            form.ShowDialog();
            PopulateSauceControl();
        }

        private void btnAddRequest_Click(object sender, EventArgs e)
        {
            var form = new EditForm(new Entree
            {
                Id = 99,
                Quantity = 1,
                Type = Models.Type.REQUEST
            });

            form.EntreeEdited += RequestEdited;
            form.ShowDialog();
            PopulateRequestControl();
        }
    }
}