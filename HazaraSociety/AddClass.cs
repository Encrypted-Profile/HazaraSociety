using HazaraSociety.App_Code;
using System;
using System.Data;
using System.Windows.Forms;

namespace HazaraSociety
{
    public partial class AddClass : Form
    {
        public AddClass()
        {
            InitializeComponent();
            loadData();
        }
        public void loadData()
        {
            Classes obj = new Classes();
            DataTable dt = obj.getAllClasses();
            classGrid.DataSource = dt;
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
                string clsCode = (string.IsNullOrWhiteSpace(txtCode.Text)) ? "" : txtCode.Text;
                if (clsCode.Length == 0)
                {
                    Error er = new Error("Please provide class code");
                    er.ShowDialog();
                }
                else
                {
                    string clsName = (string.IsNullOrWhiteSpace(txtName.Text)) ? "Not Provided" : txtName.Text;
                    int level = Int32.Parse((string.IsNullOrWhiteSpace(txtLevel.Text)) ? "0" : txtLevel.Text);
                    Classes obj = new Classes();
                    obj.classCode = clsCode;
                    obj.level = level;
                    obj.className = clsName;
                    obj.addNewClass();
                    resetData();
                    loadData();
                }

            }
            catch (Exception ex)
            {
                Error er = new Error("Error Adding Class \n May be duplication issue");
                er.ShowDialog();
            }
        }
        public void resetData()
        {
            txtName.Text = "";
            txtLevel.Text = "";
            txtCode.Text = "";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string code = classGrid.SelectedRows[0].Cells[0].Value.ToString();
                Classes obj = new Classes();
                obj.classCode = code;
                obj.delete();
                loadData();
            }
            catch (Exception ex)
            {
                Error er = new Error("Error deleting data");
                er.ShowDialog();
            }
        }
    }
}
