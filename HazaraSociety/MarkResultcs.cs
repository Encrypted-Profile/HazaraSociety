using System;
using System.Windows.Forms;

namespace HazaraSociety
{
    public partial class MarkResultcs : Form
    {
        public MarkResultcs()
        {
            InitializeComponent();
            if (!panelX.Controls.Contains(MarkResult.Instance))
            {
                panelX.Controls.Add(MarkResult.Instance);
                MarkResult.Instance.Dock = DockStyle.Fill;
                MarkResult.Instance.BringToFront();
            }
            else
            {
                MarkResult.Instance.BringToFront();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
