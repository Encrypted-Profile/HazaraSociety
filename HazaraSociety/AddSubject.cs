using HazaraSociety.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace HazaraSociety
{
    public partial class AddSubject : Form
    {
        public AddSubject()
        {
            InitializeComponent();
            loadClass();
            loadData();
            loadTeacher();

        }
        public void loadData()
        {
            Subject obj = new Subject();
            DataTable dt = obj.getAllSubjects();
            subjectGrid.DataSource = dt;
        }
        public void loadTeacher()
        {
            Dictionary<string, string> test = new Dictionary<string, string>();
            Teacher obj = new Teacher();
            try
            {
                DataTable dt = obj.getAllTeachers();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    test.Add(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString());
                }

                cboTeacher.DataSource = new BindingSource(test, null);
                cboTeacher.DisplayMember = "Value";
                cboTeacher.ValueMember = "Key";

                cboTeacher.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Error objx = new Error("Error while loading teacher data");
                objx.ShowDialog();
            }
        }
        public void loadClass()
        {
            Dictionary<string, string> test = new Dictionary<string, string>();
            Classes obj = new Classes();
            try
            {
                DataTable dt = obj.getAllClasses();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    test.Add(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString());
                }

                cbClass.DataSource = new BindingSource(test, null);
                cbClass.DisplayMember = "Value";
                cbClass.ValueMember = "Key";

                cbClass.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Error objx = new Error("Error while loading class data");
                objx.ShowDialog();
            }

        }
        private void cbClass_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void bunifuCustomTextbox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSectionId_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string code = (string.IsNullOrWhiteSpace(txtCode.Text)) ? "" : txtCode.Text;
                if (code.Length == 0)
                {
                    Error err = new Error("Subject code needed");
                    err.ShowDialog();
                }
                else
                {
                    string name = (string.IsNullOrWhiteSpace(txtName.Text)) ? "Not Provided" : txtName.Text;
                    double marks = double.Parse((string.IsNullOrWhiteSpace(txtMarks.Text)) ? "0" : txtMarks.Text);
                    double pMakrs = double.Parse((string.IsNullOrWhiteSpace(txtPassingMarks.Text)) ? "0" : txtPassingMarks.Text);
                    string cls = ((KeyValuePair<string, string>)cbClass.SelectedItem).Key;
                    string teacher = ((KeyValuePair<string, string>)cboTeacher.SelectedItem).Key;
                    Subject obj = new Subject();
                    obj.subjectCode = code;
                    obj.subjectName = name;
                    obj.subjectMarks = marks;
                    obj.subjectPassingMarks = pMakrs;
                    obj.subjectClass = cls;
                    obj.teacherCode = teacher;

                    bool x = obj.checkSubject();
                    if (x)
                    {
                        obj.update();
                        Done d = new Done();
                        d.ShowDialog();
                        resetData();
                    }
                    else
                    {
                        obj.addNewSubject();
                        Done d = new Done();
                        d.ShowDialog();
                        resetData();
                    }

                }
            }
            catch (Exception ex)
            {
                Error err = new Error("Error submitting subject data");
                err.ShowDialog();
            }
        }
        public void resetData()
        {
            txtCode.Text = "";
            txtMarks.Text = "";
            txtName.Text = "";
            txtPassingMarks.Text = "";

            loadData();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                YesNo y = new YesNo();
                y.ShowDialog();
                if (Common.yesno == true)
                {
                    string code = subjectGrid.SelectedRows[0].Cells[0].Value.ToString();
                    Subject obj = new Subject();
                    obj.subjectCode = code;
                    obj.delete();
                    resetData();
                    loadData();

                }
                else
                {

                }
            }
            catch (Exception edx)
            {
                Error obj = new Error("Sorry could not delete");
                obj.ShowDialog();
            }
        }

        private void subjectGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtCode.Text = subjectGrid.SelectedRows[0].Cells[0].Value.ToString();
                txtName.Text = subjectGrid.SelectedRows[0].Cells[1].Value.ToString();
                txtMarks.Text = subjectGrid.SelectedRows[0].Cells[2].Value.ToString();
                txtPassingMarks.Text = subjectGrid.SelectedRows[0].Cells[3].Value.ToString();
                string cls = subjectGrid.SelectedRows[0].Cells[4].Value.ToString();
                string tch = subjectGrid.SelectedRows[0].Cells[5].Value.ToString();
                int c = 0;
                for (int x = 0; x < cbClass.Items.Count; x++)
                {

                    string str = ((KeyValuePair<string, string>)cbClass.Items[x]).Key;
                    if (cls.Equals(str))
                    {
                        cbClass.SelectedIndex = c;
                        break;
                    }
                    c++;
                }


                c = 0;
                for (int x = 0; x < cboTeacher.Items.Count; x++)
                {

                    string str = ((KeyValuePair<string, string>)cboTeacher.Items[x]).Key;
                    if (tch.Equals(str))
                    {
                        cboTeacher.SelectedIndex = c;
                        break;
                    }
                    c++;
                }
            }
            catch (Exception edx)
            {
                Error obj = new Error("Please select a row");
                obj.ShowDialog();
            }
        }
    }
}
