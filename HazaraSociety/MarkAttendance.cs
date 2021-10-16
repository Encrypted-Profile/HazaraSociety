using System;
using System.Windows.Forms;

namespace HazaraSociety
{
    public partial class MarkAttendance : Form
    {
        public MarkAttendance()
        {
            InitializeComponent();

            if (!panelX.Controls.Contains(MarkAttendanceControl.Instance))
            {
                panelX.Controls.Add(MarkAttendanceControl.Instance);
                MarkAttendanceControl.Instance.Dock = DockStyle.Fill;
                MarkAttendanceControl.Instance.BringToFront();
            }
            else
            {
                MarkAttendanceControl.Instance.BringToFront();
            }

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }
    }
}
