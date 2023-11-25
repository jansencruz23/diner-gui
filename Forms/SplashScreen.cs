namespace Diner
{
    public partial class SplashScreen : Form
    {
        public SplashScreen()
        {
            InitializeComponent();
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 35, 35));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pnlloading.Width += 5;
            pnltitle.Width += 4;

            if (pnlloading.Width >= 760 && pnltitle.Width >= 580)
            {
                timer1.Stop();
                Hide();
                var form = new MainForm();
                form.FormClosed += (s, args) => Close();
                form.Show();
            }
        }
    }
}
