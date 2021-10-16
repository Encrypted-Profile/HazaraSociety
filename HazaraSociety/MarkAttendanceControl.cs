using HazaraSociety.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace HazaraSociety
{
    public partial class MarkAttendanceControl : UserControl
    {
        private static MarkAttendanceControl _instance;
        public static MarkAttendanceControl Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MarkAttendanceControl();
                }
                return _instance;
            }
        }
        public MarkAttendanceControl()
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
                for (int i = 0; i < dt.Rows.Count; i++)
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

        DataGridViewTextBoxColumn txtMarks = new DataGridViewTextBoxColumn();
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
                    DataTable dt = new DataTable();
                    dt = reg.getStudentsForMarking();


                    if (markGrid.Columns.Contains(txtMarks))
                    {
                        markGrid.Columns.Remove(txtMarks);
                    }

                    markGrid.DataSource = dt;
                    txtMarks.HeaderText = "Session Attendance";
                    markGrid.Columns.Insert(3, txtMarks);

                    for (int i = 0; i < markGrid.Rows.Count; i++)
                    {
                        int roll = Int32.Parse(markGrid.Rows[i].Cells[0].Value.ToString());
                        Attendance md = new Attendance();
                        md.rollNumber = roll;
                        md.year = year;
                        md.examCode = exam;
                        bool x = md.check();
                        if (x)
                        {
                            DataTable d = md.getMarks();
                            markGrid.Rows[i].Cells[3].Value = d.Rows[0][0].ToString();
                        }
                        else
                        {
                            markGrid.Rows[i].Cells[3].Value = "0";
                        }
                    }
                    //markGrid.Columns[0].ReadOnly = true;
                    //markGrid.Columns[1].ReadOnly = true;
                    //markGrid.Columns[2].ReadOnly = true;
                    //markGrid.Columns[3].ReadOnly = true;
                    //markGrid.Columns[4].ReadOnly = true;

                }
            }
            catch (Exception ex)
            {
                Error objx = new Error("Unknow Error");
                objx.ShowDialog();
                //MessageBox.Show(ex.ToString());
            }
        }

        private void cbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadSections();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            if (markGrid.Rows.Count == 0)
            {
                Error objx = new Error("No Data To Be Updated");
                objx.ShowDialog();
            }
            else
            {
                Attendance md = new Attendance();
                md.year = year;
                md.examCode = exam;

                int loc = -1;
                for (int j = 0; j < markGrid.Rows.Count; j++)
                {
                    if (double.Parse(markGrid.Rows[j].Cells[3].Value.ToString()) > 100)
                    {
                        loc = j;
                        break;
                    }
                }
                if (loc < 0)
                {
                    for (int i = 0; i < markGrid.Rows.Count; i++)
                    {
                        md.rollNumber = Int32.Parse(markGrid.Rows[i].Cells[0].Value.ToString());
                        md.attendance = double.Parse(markGrid.Rows[i].Cells[3].Value.ToString());
                        if (md.check())
                        {
                            md.updateData();
                        }
                        else
                        {
                            md.addAttendance();
                        }
                    }

                    Done d = new Done();
                    d.ShowDialog();
                    resetData();

                }
                else
                {
                    Error objx = new Error("Invalid data entry at row " + loc);
                    objx.ShowDialog();
                }

            }
        }
        public void resetData()
        {
            if (markGrid.Columns.Contains(txtMarks))
            {
                markGrid.Columns.Remove(txtMarks);
            }
            markGrid.Columns.Clear();
        }
    }
}
