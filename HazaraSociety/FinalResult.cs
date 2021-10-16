using HazaraSociety.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace HazaraSociety
{
    public partial class FinalResult : Form
    {
        public FinalResult()
        {
            InitializeComponent();
            loadClass();
            loadSections();
        }
        public void loadSections()
        {
            cboSection.Items.Clear();
            string cls = ((KeyValuePair<string, string>)cbClass.SelectedItem).Key;
            Section obj = new Section();
            obj.sectionClass = cls;
            DataTable dt = obj.getSetionByClass();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cboSection.Items.Add(dt.Rows[i][1].ToString());
            }
            if (cboSection.Items.Count > 0)
                cboSection.SelectedIndex = 0;

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
                //loadSubjects();
            }
            catch (Exception ex)
            {
                Error objx = new Error("Error while loading class data");
                objx.ShowDialog();
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

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void cbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadSections();
        }
        DataTable dt;
        string clsCode;
        string cls;
        string sec;
        string exam;
        int year;
        private void button2_Click_1(object sender, EventArgs e)
        {
            clsCode = "";
            cls = "";
            sec = "";
            exam = "";
            year = 0;

            try
            {
                year = Int32.Parse((string.IsNullOrWhiteSpace(txtYear.Text)) ? "0" : txtYear.Text);
                if (year == 0)
                {
                    Error objx = new Error("Year is incorrect\ncan not be zero");
                    objx.ShowDialog();
                }
                else
                {
                    clsCode = ((KeyValuePair<string, string>)cbClass.SelectedItem).Key;
                    cls = ((KeyValuePair<string, string>)cbClass.SelectedItem).Value;
                    sec = cboSection.SelectedItem.ToString();

                    exam = "Annual Final Result";

                    Registration reg = new Registration();
                    reg.classCode = clsCode;
                    reg.sectionName = sec;
                    reg.year = year;
                    dt = new DataTable();
                    dt = reg.getStudentsForMarking();


                }
            }
            catch (Exception ex)
            {
                //Error objx = new Error("Year is incorrect\ncan not be zero");
                //objx.ShowDialog();
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
