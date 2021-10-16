using System;
using System.Windows.Forms;

namespace HazaraSociety
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();


            if (!panelX.Controls.Contains(WelcomePage.Instance))
            {
                panelX.Controls.Add(WelcomePage.Instance);
                WelcomePage.Instance.Dock = DockStyle.Fill;
                WelcomePage.Instance.BringToFront();
            }
            else
            {
                WelcomePage.Instance.BringToFront();
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void lblTime_Click(object sender, EventArgs e)
        {
            if (!panelX.Controls.Contains(WelcomePage.Instance))
            {
                panelX.Controls.Add(WelcomePage.Instance);
                WelcomePage.Instance.Dock = DockStyle.Fill;
                WelcomePage.Instance.BringToFront();
            }
            else
            {
                WelcomePage.Instance.BringToFront();
            }
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            if (!panelX.Controls.Contains(RegisterNewStudent.Instance))
            {
                panelX.Controls.Add(RegisterNewStudent.Instance);
                RegisterNewStudent.Instance.Dock = DockStyle.Fill;
                RegisterNewStudent.Instance.BringToFront();
            }
            else
            {
                RegisterNewStudent.Instance.BringToFront();
            }
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            if (!panelX.Controls.Contains(ManageStudents.Instance))
            {
                panelX.Controls.Add(ManageStudents.Instance);
                ManageStudents.Instance.Dock = DockStyle.Fill;
                ManageStudents.Instance.BringToFront();
            }
            else
            {
                ManageStudents.Instance.BringToFront();
            }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if (!panelX.Controls.Contains(StudentManagement.Instance))
            {
                panelX.Controls.Add(StudentManagement.Instance);
                StudentManagement.Instance.Dock = DockStyle.Fill;
                StudentManagement.Instance.BringToFront();
            }
            else
            {
                StudentManagement.Instance.BringToFront();
            }
        }

        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
            if (!panelX.Controls.Contains(ExamSection.Instance))
            {
                panelX.Controls.Add(ExamSection.Instance);
                ExamSection.Instance.Dock = DockStyle.Fill;
                ExamSection.Instance.BringToFront();
            }
            else
            {
                ExamSection.Instance.BringToFront();
            }
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            MarkResultcs obj = new MarkResultcs();
            obj.ShowDialog();

        }
    }
}
