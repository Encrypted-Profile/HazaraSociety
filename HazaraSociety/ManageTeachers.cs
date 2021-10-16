using HazaraSociety.App_Code;
using System;
using System.Data;
using System.Windows.Forms;

namespace HazaraSociety
{
    public partial class ManageTeachers : Form
    {
        public ManageTeachers()
        {
            InitializeComponent();
            loadData();
        }
        public void loadData()
        {
            Teacher obj = new Teacher();
            DataTable dt = obj.getAllTeachers();
            teacherGrid.DataSource = dt;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }

        private void teacherGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string code = teacherGrid.SelectedRows[0].Cells[0].Value.ToString();
                Teacher obj = new Teacher();
                obj.teacherCode = code;
                DataTable dt = obj.getTeacherByCode();
                txtCode.Text = dt.Rows[0][0].ToString();
                txtName.Text = dt.Rows[0][1].ToString();

                int count = 0;
                foreach (string li in cboGender.Items)
                {
                    if (li == dt.Rows[0][2].ToString())
                    {
                        cboGender.selectedIndex = count;
                    }
                    count++;
                }

                txtAddress.Text = dt.Rows[0][4].ToString();
                txtMobile.Text = dt.Rows[0][3].ToString();
                txtEmail.Text = dt.Rows[0][5].ToString();
                txtQualification.Text = dt.Rows[0][6].ToString();
                txtReligion.Text = dt.Rows[0][8].ToString();
            }
            catch (Exception ex)
            {
                Error x = new Error("Sory Error Occured");
                x.ShowDialog();
            }
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
                    obj.teacherName = (string.IsNullOrWhiteSpace(txtName.Text)) ? "Not Provided" : txtName.Text;
                    obj.gender = cboGender.selectedValue.ToString();
                    obj.mobile = (string.IsNullOrWhiteSpace(txtMobile.Text)) ? "Not Provided" : txtMobile.Text;
                    obj.email = (string.IsNullOrWhiteSpace(txtEmail.Text)) ? "Not Provided" : txtEmail.Text;
                    obj.qualification = (string.IsNullOrWhiteSpace(txtQualification.Text)) ? "Not Provided" : txtQualification.Text;
                    obj.religion = (string.IsNullOrWhiteSpace(txtReligion.Text)) ? "Not Provided" : txtReligion.Text;
                    obj.address = (string.IsNullOrWhiteSpace(txtAddress.Text)) ? "Not Provided" : txtAddress.Text;

                    obj.updateTeacher();
                    Done d = new Done();
                    d.ShowDialog();
                    resetData();
                    loadData();
                }
            }
            catch (Exception ex)
            {
                Error x = new Error("Could not update teacher data");
                x.ShowDialog();
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string code = teacherGrid.SelectedRows[0].Cells[0].Value.ToString();
                Teacher obj = new Teacher();
                obj.teacherCode = code;
                YesNo y = new YesNo();
                y.ShowDialog();
                if (Common.yesno == true)
                {
                    obj.delete();
                    resetData();
                    loadData();
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                Error x = new Error("CCould not delete");
                x.ShowDialog();
            }
        }
    }
}
