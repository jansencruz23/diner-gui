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

        private void PopulateControl(Models.Type entreeType, Action<EntreeControl> eventHandler)
        {
            _entreeControls.Clear();
            panelBody.Controls.Clear();

            foreach (var entree in _entrees.Where(e => e.Type == entreeType))
            {
                var entreeControl = new EntreeControl(entree);
                eventHandler?.Invoke(entreeControl);
                _entreeControls.Add(entreeControl);
            }

            var entreeView = new EntreesView(_entreeControls);
            entreeView.Dock = DockStyle.Fill;
            panelBody.Controls.Add(entreeView);
        }

        private void PopulateEntreeControl()
        {
            PopulateControl(Models.Type.ENTREE, entreeControl =>
                entreeControl.EditEntreeClicked += EntreeControl_EditEntreeClicked);
        }

        private void PopulateSauceControl()
        {
            PopulateControl(Models.Type.SAUCE, entreeControl =>
                entreeControl.EditSauceClicked += EntreeControl_EditSauceClicked);
        }

        private void PopulateRequestControl()
        {
            PopulateControl(Models.Type.REQUEST, entreeControl =>
                entreeControl.EditRequestClicked += EntreeControl_EditRequestClicked);
        }

        private void PopulateDrinkControl()
        {
            PopulateControl(Models.Type.DRINK, entreeControl =>
                entreeControl.EditDrinkClicked += EntreeControl_EditDrinkClicked);
        }

        private void RemoveEntree(Models.Type entreeType, string name)
        {
            var entree = _entrees.FirstOrDefault(e => e.Type == entreeType
                && e.Name.Equals(name));

            if (entree != null)
            {
                _entrees.Remove(entree);

                switch (entreeType)
                {
                    case Models.Type.ENTREE:
                        PopulateEntreeControl();
                        break;
                    case Models.Type.SAUCE:
                        PopulateSauceControl();
                        break;
                    case Models.Type.REQUEST:
                        PopulateRequestControl();
                        break;
                }
            }
        }

        private void RemoveEntree(Models.Type entreeType, int id)
        {
            var entree = _entrees.FirstOrDefault(e => e.Type == entreeType 
                && e.Id == id);

            if (entree != null)
            {
                _entrees.Remove(entree);
                PopulateEntreeControl();
            }
        }

        private void EntreeDeleted(object? sender, EntreeEventArgs e)
        {
            RemoveEntree(Models.Type.ENTREE, e.Entree.Id);
        }

        private void SauceDeleted(object? sender, EntreeEventArgs e)
        {
            RemoveEntree(Models.Type.SAUCE, e.Entree.Name);
        }

        private void RequestDeleted(object? sender, EntreeEventArgs e)
        {
            RemoveEntree(Models.Type.REQUEST, e.Entree.Name);
        }

        private void EntreeControl_EditClicked(object? sender, EntreeControlEventArgs e, 
            EventHandler<EntreeEventArgs> editedHandler,
            EventHandler<EntreeEventArgs> deletedHandler = null)
        {
            var editForm = new EditForm(e.EntreeControl.Entree);
            editForm.EntreeEdited += editedHandler;

            if (deletedHandler != null)
            {
                editForm.EntreeDeleted += (s, args) => deletedHandler(s, args);
            }

            editForm.ShowDialog();
        }

        private void EntreeControl_EditEntreeClicked(object? sender, EntreeControlEventArgs e) =>
            EntreeControl_EditClicked(sender, e, 
                EntreeEdited, EntreeDeleted);

        private void EntreeControl_EditSauceClicked(object? sender, EntreeControlEventArgs e) =>
            EntreeControl_EditClicked(sender, e, 
                SauceEdited, SauceDeleted);

        private void EntreeControl_EditRequestClicked(object? sender, EntreeControlEventArgs e) =>
            EntreeControl_EditClicked(sender, e, 
                RequestEdited, RequestDeleted);

        private void EntreeControl_EditDrinkClicked(object? sender, EntreeControlEventArgs e) =>
            EntreeControl_EditClicked(sender, e, 
                DrinksEdited);

        private void HandleEntreeEdited(Models.Type entreeType, Func<Entree, bool> criteria, 
            Action<EntreeControl> clickHandler, Action populateControl, EntreeEventArgs e)
        {
            var existingEntree = _entrees
                .FirstOrDefault(i => i.Type == entreeType && criteria(i));

            if (existingEntree != null)
            {
                _entrees.Remove(existingEntree);
            }

            _entrees.Add(e.Entree);

            var entreeControl = new EntreeControl(e.Entree);
            clickHandler(entreeControl);
            _entreeControls.Add(entreeControl);

            populateControl();
        }

        private void EntreeEdited(object? sender, EntreeEventArgs e) =>
            HandleEntreeEdited(Models.Type.ENTREE, i => i.Id == e.Entree.Id, 
                ec => ec.EditEntreeClicked += EntreeControl_EditEntreeClicked, PopulateEntreeControl, e);

        private void SauceEdited(object? sender, EntreeEventArgs e) =>
            HandleEntreeEdited(Models.Type.SAUCE, i => i.Name.Equals(e.Entree.Name), 
                ec => ec.EditEntreeClicked += EntreeControl_EditSauceClicked, PopulateSauceControl, e);

        private void RequestEdited(object? sender, EntreeEventArgs e) =>
            HandleEntreeEdited(Models.Type.REQUEST, i => i.Name.Equals(e.Entree.Name),
                ec => ec.EditRequestClicked += EntreeControl_EditRequestClicked, PopulateRequestControl, e);

        private void DrinksEdited(object? sender, EntreeEventArgs e) =>
            HandleEntreeEdited(Models.Type.DRINK, i => i.Name.Equals(e.Entree.Name), 
                ec => ec.EditDrinkClicked += EntreeControl_EditDrinkClicked, PopulateDrinkControl, e);

        private void MenuButtonClicked(Guna2Button button, System.Windows.Forms.Timer timer, 
            Action controlPopulation)
        {
            CheckMenuExpand();
            ColorButton(button);
            CloseTabs(ref _entreeExpand, timerEntrees);
            CloseTabs(ref _sauceExpand, timerSauces);
            CloseTabs(ref _requestExpand, timerRequests);

            if (timer != null)
            {
                timer.Start();
            }

            controlPopulation();
        }

        private void btnEntree_Click(object sender, EventArgs e)
        {
            MenuButtonClicked(btnEntree, timerEntrees, PopulateEntreeControl);
        }

        private void btnDrinks_Click(object sender, EventArgs e)
        {
            MenuButtonClicked(btnDrinks, null, PopulateDrinkControl);
        }

        private void btnSauce_Click(object sender, EventArgs e)
        {
            MenuButtonClicked(btnSauce, timerSauces, PopulateSauceControl);
        }

        private void btnRequest_Click(object sender, EventArgs e)
        {
            MenuButtonClicked(btnRequest, timerRequests, PopulateRequestControl);
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
            var prompt = MessageDialog.Show(this, "Are you sure to log out?", "Admin Logout",
                MessageDialogButtons.YesNo, MessageDialogIcon.Question,
                MessageDialogStyle.Dark);

            if (prompt == DialogResult.Yes)
            {
                Hide();
                var form = new MainForm(_entrees);
                form.FormClosed += (s, args) => Close();
                form.Show();
            }
        }

        private void btnAddEntree_Click(object sender, EventArgs e)
        {
            var form = new EditForm(new Entree
            {
                Id = _entrees.Count + 1,
                Quantity  = 1,
                Type = Models.Type.ENTREE
            }, true);

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
            }, true);

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
            }, true);

            form.EntreeEdited += RequestEdited;
            form.ShowDialog();
            PopulateRequestControl();
        }
    }
}