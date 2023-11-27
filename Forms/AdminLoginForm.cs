using Diner.Models;
using Guna.UI2.WinForms;

namespace Diner.Forms
{
    public partial class AdminLoginForm : Form
    {
        private readonly Action _hide;
        private readonly List<Entree> _entrees;
        public AdminLoginForm(Action hide,
            List<Entree> entrees)
        {
            _hide = hide;
            _entrees = entrees;
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var username = txtUsername.Text;
            var password = txtPassword.Text;

            if (username.Equals("admin") && password.Equals("admin"))
            {
                _hide.Invoke();
                var adminForm = new AdminForm(_entrees);
                adminForm.FormClosed += (s, args) => Close();
                adminForm.Show();
                Hide();
            }
            else
            {
                MessageDialog.Show(this, "Admin credentials is incorrect", "Admin Login Failed",
                    MessageDialogButtons.OK, MessageDialogIcon.Error, MessageDialogStyle.Dark);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
