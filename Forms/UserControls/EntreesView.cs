using System.Globalization;

namespace Diner.Forms.UserControls
{
    public partial class EntreesView : UserControl
    {
        private List<EntreeControl> _entreesControls;
        public EntreesView(List<EntreeControl> entreeControls)
        {
            _entreesControls = entreeControls;
            InitializeComponent();
            InitializeEntrees();
            InitializeTitle();
        }

        private void InitializeEntrees()
        {
            foreach (var entree in _entreesControls)
            {
                flowPanel.Controls.Add(entree);
            }
        }

        private void InitializeTitle()
        {
            var entree = _entreesControls.FirstOrDefault();
            string type = entree.Entree.Type.ToString();
            string capitalizedType = string.IsNullOrEmpty(type)
                ? "Entree"
                : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(type.ToLower());

            lblType.Text = capitalizedType;
        }
    }
}