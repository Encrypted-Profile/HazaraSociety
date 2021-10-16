using HazaraSociety.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace HazaraSociety
{
    public partial class SelectClass : Form
    {
        public SelectClass()
        {
            InitializeComponent();
            loadClass();
            loadSections();
            loadExams();
        }
        public void loadExams()
        {
            Dictionary<string, string> test = new Dictionary<string, string>();
            Exam obj = new Exam();
            try
            {
                DataTable dt = obj.getExams();
                int i;
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    test.Add(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString());
                }

                cboExams.DataSource = new BindingSource(test, null);
                cboExams.DisplayMember = "Value";
                cboExams.ValueMember = "Key";

                cboExams.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Error objx = new Error("Error while loading exam data");
                objx.ShowDialog();
            }
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

        private void bunifuCustomLabel11_Click(object sender, EventArgs e)
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
        private void button2_Click(object sender, EventArgs e)
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

                    exam = ((KeyValuePair<string, string>)cboExams.SelectedItem).Key;

                    Registration reg = new Registration();
                    reg.classCode = clsCode;
                    reg.sectionName = sec;
                    reg.year = year;
                    dt = new DataTable();
                    dt = reg.getStudentsForMarkingPosition(year, exam);
                    markGrid.DataSource = dt;
                    markGrid.Columns[0].ReadOnly = true;
                    markGrid.Columns[1].ReadOnly = true;
                    markGrid.Columns[2].ReadOnly = true;
                    markGrid.Columns[3].ReadOnly = true;
                    markGrid.Columns[4].ReadOnly = true;

                }
            }
            catch (Exception ex)
            {
                Error objx = new Error("Unknow Error");
                objx.ShowDialog();
                //MessageBox.Show(ex.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (markGrid.Rows.Count <= 0)
            {
                Error error = new Error("No Data Found");
                error.ShowDialog();
            }
            {
                try
                {
                    Attendance pp = new Attendance();


                    string[] pos = { "1st", "2nd", "3rd", "4th", "5th", "6th", "7th", "8th", "9th", "10th", "11th", "12th", "13th", "14th", "15th", "16th", "17th", "18th", "19th", "20th", "21st", "22nd", "23rd", "24th", "25th", "26th", "27th", "28th", "29th", "30th", "31st", "32nd", "33rd", "34th", "35th", "36th", "37th", "38th", "39th", "40th", "41st", "42nd", "43rd", "44th", "45th", "46th", "47th", "48th", "49th", "50th", "51st", "52nd", "53rd", "54th", "55th", "56th", "57th", "58th", "59th", "60th", "61st", "62nd", "63rd", "64th", "65th", "66th", "67th", "68th", "69th", "70th", "71st", "72nd", "73rd", "74th", "75th", "76th", "77th", "78th", "79th", "80th", "81st", "82nd", "83rd", "84th", "85th", "86th", "87th", "88th", "89th", "90th", "91st", "92nd", "93rd", "94th", "95th", "96th", "97th", "98th", "99th", "100th" };

                    //--------------------------
                    double temp = -1;
                    int count = 0;
                    pp.examCode = exam;
                    pp.year = year;
                    for (int i = 0; i < markGrid.Rows.Count; i++)
                    {
                        pp.rollNumber = Int32.Parse(markGrid.Rows[i].Cells[0].Value.ToString());
                        double m = double.Parse(markGrid.Rows[i].Cells[3].Value.ToString());
                        if (m == temp)
                        {
                            count--;
                            pp.position = pos[count];
                            pp.updatePosition();
                            count++;
                        }
                        else
                        {
                            pp.position = pos[count];
                            pp.updatePosition();
                            count++;

                        }
                    }
                    //--------------------------
                }
                catch (Exception ex)
                {
                    //Error objx = new Error("Unknow Error");
                    //objx.ShowDialog();
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
