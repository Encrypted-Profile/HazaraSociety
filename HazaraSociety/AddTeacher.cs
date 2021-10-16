using HazaraSociety.App_Code;
using System;
using System.Windows.Forms;

namespace HazaraSociety
{
    public partial class AddTeacher : Form
    {
        public AddTeacher()
        {
            InitializeComponent();
            cboGender.selectedIndex = 0;
            loadTeachers();
        }
        public void loadTeachers()
        {


        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string code = (string.IsNullOrWhiteSpace(txtCode.Text)) ? "" : txtCode.Text;
                if (code.Length == 0)
                {
                    Error error = new Error("Please provide teacher code");
                    error.ShowDialog();
                }
                else
                {
                    Teacher obj = new Teacher();
                    obj.teacherCode = code;
                    bool x = obj.checkTeacher();
                    if (x)
                    {
                        Error error = new Error("Teacher with the code " + code + "already exists\nTry new code");
                        error.ShowDialog();
                    }
                    else
                    {
                        obj.teacherCode = code;
                        obj.teacherName = (string.IsNullOrWhiteSpace(txtName.Text)) ? "Not Provided" : txtName.Text;
                        obj.gender = cboGender.selectedValue.ToString();
                        obj.mobile = (string.IsNullOrWhiteSpace(txtMobile.Text)) ? "Not Provided" : txtMobile.Text;
                        obj.email = (string.IsNullOrWhiteSpace(txtEmail.Text)) ? "Not Provided" : txtEmail.Text;
                        obj.qualification = (string.IsNullOrWhiteSpace(txtQualification.Text)) ? "Not Provided" : txtQualification.Text;
                        obj.dob = (string.IsNullOrWhiteSpace(txtDOB.Value.ToShortDateString())) ? DateTime.Now.ToShortDateString() : txtJoiningDate.Value.ToShortDateString();
                        obj.joiningDate = (string.IsNullOrWhiteSpace(txtJoiningDate.Value.ToShortDateString())) ? DateTime.Now.ToShortDateString() : txtDOB.Value.ToShortDateString();
                        obj.religion = (string.IsNullOrWhiteSpace(txtReligion.Text)) ? "Not Provided" : txtReligion.Text;
                        obj.address = (string.IsNullOrWhiteSpace(txtAddress.Text)) ? "Not Provided" : txtAddress.Text;

                        obj.addTeacher();
                        Done d = new Done();
                        d.ShowDialog();
                        resetData();
                    }
                }
            }
            catch (Exception ex)
            {
                Error er = new Error(ex.ToString());
                er.ShowDialog();
            }
        }
        public void resetData()
        {
            txtAddress.Text = "";
            txtCode.Text = "";
            txtEmail.Text = "";
            txtMobile.Text = "";
            txtName.Text = "";
            txtQualification.Text = "";
            txtReligion.Text = "";
        }
    }
}
