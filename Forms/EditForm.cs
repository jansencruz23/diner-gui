using Diner.CustomEventArgs;
using Diner.Models;
using Guna.UI2.WinForms;

namespace Diner.Forms
{
    public partial class EditForm : Form
    {
        public EventHandler<EntreeEventArgs> EntreeEdited;
        public EventHandler<EntreeEventArgs> EntreeDeleted;
        private Entree _entree;

        public EditForm(Entree entree,
            bool newEntree = false)
        {
            _entree = entree;
            InitializeComponent();
            InitializeEntree();

            if (entree.Type == Models.Type.DRINK
                || newEntree)
            {
                btnDelete.Enabled = false;
            }
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
                if (string.IsNullOrWhiteSpace(txtName.Text)
                    || imgPic.Image == null)
                {
                    MessageDialog.Show(this, "Invalid input. Try again",
                        "Save Failed", MessageDialogButtons.OK,
                        MessageDialogIcon.Error, MessageDialogStyle.Dark);
                    return;
                }

                _entree.Price = Convert.ToDouble(txtPrice.Text);
                _entree.Name = txtName.Text;
                _entree.Image = imgPic.Image;

                EntreeEdited?.Invoke(this, new EntreeEventArgs(_entree));
                Close();
            }
            catch
            {
                MessageDialog.Show(this, "Invalid input. Try again",
                    "Save Failed", MessageDialogButtons.OK,
                    MessageDialogIcon.Error, MessageDialogStyle.Dark);
            }
        }

        private void imgPic_Click(object sender, EventArgs e)
        {
            using var file = new OpenFileDialog();
            file.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.ico|All Files|*.*";
            file.FilterIndex = 1;
            file.RestoreDirectory = true;

            if (file.ShowDialog() == DialogResult.OK)
            {
                imgPic.Image = Image.FromFile(file.FileName);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            EntreeDeleted?.Invoke(this, new EntreeEventArgs(_entree));
            Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
