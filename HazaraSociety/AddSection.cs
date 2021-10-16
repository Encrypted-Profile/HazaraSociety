using HazaraSociety.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace HazaraSociety
{
    public partial class cboClass : Form
    {
        public cboClass()
        {
            InitializeComponent();
            loadData();
            loadClass();
            loadTeachers();
        }
        public void loadData()
        {
            Section obj = new Section();
            DataTable dt = obj.getAllActiveClasses();
            sectionGrid.DataSource = dt;
        }
        public void loadTeachers()
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

                cboTeachers.DataSource = new BindingSource(test, null);
                cboTeachers.DisplayMember = "Value";
                cboTeachers.ValueMember = "Key";

                cboTeachers.SelectedIndex = 0;
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
                Section obj = new Section();
                string id = (string.IsNullOrWhiteSpace(txtSectionId.Text)) ? "" : txtSectionId.Text;
                string name = (string.IsNullOrWhiteSpace(txtName.Text)) ? "Not Provided" : txtName.Text;
                string cls = ((KeyValuePair<string, string>)cbClass.SelectedItem).Key;
                string inch = ((KeyValuePair<string, string>)cboTeachers.SelectedItem).Key;
                bool x;
                int year = Int32.Parse((string.IsNullOrWhiteSpace(txtYear.Text)) ? "0" : txtYear.Text);
                if (chkStatus.Checked == true)
                {
                    x = true;
                }
                else
                {
                    x = false;
                }
                if (id.Length == 0)
                {
                    //-insert
                    obj.sectionName = name;
                    obj.sectionClass = cls;
                    obj.sectionIncharge = inch;
                    obj.sectionStatus = x;
                    obj.year = year;

                    obj.addNewSection();
                    Done d = new Done();
                    d.ShowDialog();
                    resetData();
                    loadData();
                }
                else
                {
                    //-update
                    obj.sectionId = Int32.Parse(id);
                    obj.sectionName = name;
                    obj.sectionClass = cls;
                    obj.sectionIncharge = inch;
                    obj.sectionStatus = x;
                    obj.year = year;

                    obj.update();
                    Done d = new Done();
                    d.ShowDialog();
                    resetData();
                    loadData();
                }
            }
            catch (Exception ex)
            {
                Error objx = new Error("Error while submitting section data");
                objx.ShowDialog();
            }
        }
        public void resetData()
        {
            txtName.Text = "";
            txtSectionId.Text = "";
            txtYear.Text = "";
        }

        private void sectionGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtSectionId.Text = sectionGrid.SelectedRows[0].Cells[0].Value.ToString();
                txtName.Text = sectionGrid.SelectedRows[0].Cells[1].Value.ToString();
                string cls = sectionGrid.SelectedRows[0].Cells[2].Value.ToString();
                string tch = sectionGrid.SelectedRows[0].Cells[3].Value.ToString();
                string chk = sectionGrid.SelectedRows[0].Cells[4].Value.ToString();
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
                for (int x = 0; x < cboTeachers.Items.Count; x++)
                {

                    string str = ((KeyValuePair<string, string>)cboTeachers.Items[x]).Key;
                    if (tch.Equals(str))
                    {
                        cboTeachers.SelectedIndex = c;
                        break;
                    }
                    c++;
                }
                if (chk.Equals("True"))
                {
                    chkStatus.Checked = true;
                }
                else
                {
                    chkStatus.Checked = true;
                }

                txtYear.Text = sectionGrid.SelectedRows[0].Cells[5].Value.ToString();

            }
            catch (Exception ex)
            {
                Error objx = new Error("Error selecting data\ntry again");
                objx.ShowDialog();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string id = sectionGrid.SelectedRows[0].Cells[0].Value.ToString();
                Section obj = new Section();
                obj.sectionId = Int32.Parse(id);
                YesNo y = new YesNo();
                y.ShowDialog();
                if (Common.yesno == true)
                {
                    obj.delete();
                    resetData();
                    loadData();
                    Done d = new Done();
                    d.ShowDialog();
                }
                else
                {
                    //------------
                }
            }
            catch (Exception ex)
            {
                Error objx = new Error("Error deleting data\ntry again");
                objx.ShowDialog();
            }
        }
    }
}
