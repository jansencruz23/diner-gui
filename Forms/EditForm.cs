using Diner.CustomEventArgs;
using Diner.Models;
using Guna.UI2.WinForms;

namespace Diner.Forms
{
    public partial class EditForm : Form
    {
        public EventHandler<EntreeEventArgs> EntreeEdited;
        private Entree _entree;
        public EditForm(Entree entree)
        {
            _entree = entree;
            InitializeComponent();
            InitializeEntree();
        }

        private void InitializeEntree()
        {
            txtName.Text = _entree.Name;
            txtPrice.Text = _entree.Price + "";
            imgPic.Image = _entree.Image;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                _entree.Price = Convert.ToDouble(txtPrice.Text);
                _entree.Name = txtName.Text;

                EntreeEdited?.Invoke(this, new EntreeEventArgs(_entree));

                Close();
            }
            catch
            {
                MessageDialog.Show("Invalid input. Try again");
            }
        }
    }
}
