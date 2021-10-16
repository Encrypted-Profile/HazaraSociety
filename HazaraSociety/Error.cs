using System.Windows.Forms;

namespace HazaraSociety
{
    public partial class Error : Form
    {
        public Error(string str)
        {
            InitializeComponent();
            lblError.Text = str;
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}
