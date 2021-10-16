using HazaraSociety.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace HazaraSociety
{
    public partial class ChangeClass : Form
    {
        int rNumber;
        int oldLevel;
        string oldCode;
        int oldYear;
        public ChangeClass(int rNumber)
        {
            this.rNumber = rNumber;
            InitializeComponent();
            loadClass();
            loadSections();
            lblRollNumber.Text = this.rNumber.ToString();
            loadData();
        }
        public void loadData()
        {
            Registration obj = new Registration();
            obj.rollNumber = this.rNumber;
            DataTable dt = obj.getActiveClassByRollNumber();
            oldCode = dt.Rows[0][0].ToString();
            lblClass.Text = dt.Rows[0][1].ToString();
            lblSection.Text = dt.Rows[0][2].ToString();
            this.oldYear = Int32.Parse(dt.Rows[0][3].ToString());

            Classes cls = new Classes();
            cls.classCode = oldCode;
            this.oldLevel = cls.getClassLevel();
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

        private void cbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadSections();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (cboSection.SelectedIndex == -1 || txtYear.Text.Length != 4)
            {
                Error er = new Error("Please select a section \nor the class does not exist or invalid year");
                er.ShowDialog();
            }
            else
            {
                string clsCode = ((KeyValuePair<string, string>)cbClass.SelectedItem).Key;
                string cls = ((KeyValuePair<string, string>)cbClass.SelectedItem).Value;
                string sec = cboSection.SelectedItem.ToString();
                Classes obj = new Classes();
                obj.classCode = clsCode;
                int newLevel = obj.getClassLevel();

                if (newLevel > oldLevel)
                {
                    //------------ update old class to not active and clear
                    Registration reg = new Registration();
                    reg.rollNumber = rNumber;
                    reg.classCode = oldCode;
                    reg.className = lblClass.Text;
                    reg.year = oldYear;
                    reg.status = "Not Active";
                    reg.resultStatus = "Cleared";
                    reg.upgrade();

                    //------------ delete if exists new class 
                    Registration x = new Registration();
                    x.rollNumber = rNumber;
                    x.classCode = clsCode;
                    x.className = cls;
                    x.sectionName = sec;
                    x.year = Int32.Parse(txtYear.Text);
                    x.status = "Active";
                    x.resultStatus = "Not Cleared";
                    x.deleteOldClass();
                    //------------ new entry
                    x.register();
                    Done d = new Done();
                    d.ShowDialog();
                    this.Close();
                }
                else
                {
                    try
                    {
                        // 1 -      delete class
                        Registration reg = new Registration();
                        reg.rollNumber = rNumber;
                        reg.classCode = oldCode;
                        reg.className = lblClass.Text;
                        reg.year = oldYear;
                        reg.deleteOldClass();
                        // 2 -     Now inser new class

                        Registration x = new Registration();
                        x.rollNumber = rNumber;
                        x.classCode = clsCode;
                        x.className = cls;
                        x.sectionName = sec;
                        x.year = Int32.Parse(txtYear.Text);
                        x.status = "Active";
                        x.resultStatus = "Not Cleared";

                        //------ delete if exists with same year
                        x.deleteOldClass();
                        //------- and insert new item
                        x.register();
                        Done d = new Done();
                        d.ShowDialog();
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        Error objx = new Error("Unknown error occured please check \nif data is updated");
                        objx.ShowDialog();
                    }
                }

            }
        }
    }
}
