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
        }

        private void InitializeEntrees()
        {
            foreach (var entree in _entreesControls)
            {
                flowPanel.Controls.Add(entree);
            }
        }
    }
}