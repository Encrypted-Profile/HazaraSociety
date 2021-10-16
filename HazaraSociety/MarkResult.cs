using HazaraSociety.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace HazaraSociety
{
    public partial class MarkResult : UserControl
    {
        private static MarkResult _instance;
        public static MarkResult Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MarkResult();
                }
                return _instance;
            }
        }
        public MarkResult()
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
        public void loadSubjects()
        {
            Dictionary<string, string> test = new Dictionary<string, string>();
            Subject obj = new Subject();
            try
            {
                obj.subjectClass = ((KeyValuePair<string, string>)cbClass.SelectedItem).Key;
                DataTable dt = obj.getSubjectsByClass();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    test.Add(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString());
                }

                cboSubjects.DataSource = new BindingSource(test, null);
                cboSubjects.DisplayMember = "Value";
                cboSubjects.ValueMember = "Key";

                cboSubjects.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                //Error objx = new Error("Error while loading subject data");
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
                //loadSubjects();
            }
            catch (Exception ex)
            {
                Error objx = new Error("Error while loading class data");
                objx.ShowDialog();
            }

        }

        private void cbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadSections();
            loadSubjects();
        }
        DataGridViewTextBoxColumn txtMarks = new DataGridViewTextBoxColumn();
        string clsCode;
        string cls;
        string sec;
        string exam;
        string subject;
        int year;

        private void button2_Click(object sender, EventArgs e)
        {
            clsCode = "";
            cls = "";
            sec = "";
            exam = "";
            subject = "";
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
                    subject = ((KeyValuePair<string, string>)cboSubjects.SelectedItem).Key;

                    Registration reg = new Registration();
                    reg.classCode = clsCode;
                    reg.sectionName = sec;
                    reg.year = year;
                    DataTable dt = new DataTable();
                    dt = reg.getStudentsForMarking();
                    dt.Columns.Add(new DataColumn("Total Marks"));
                    dt.Columns.Add(new DataColumn("Passing Marks"));

                    Subject s = new Subject();
                    s.subjectCode = subject;
                    DataTable dsub = s.getAllSubjectsByKey();
                    foreach (DataRow dr in dt.Rows)
                    {

                        dr["Total Marks"] = dsub.Rows[0][2].ToString();
                        dr["Passing Marks"] = dsub.Rows[0][3].ToString();
                    }


                    if (markGrid.Columns.Contains(txtMarks))
                    {
                        markGrid.Columns.Remove(txtMarks);
                    }

                    markGrid.DataSource = dt;
                    txtMarks.HeaderText = "Obtained Marks";
                    markGrid.Columns.Insert(5, txtMarks);

                    for (int i = 0; i < markGrid.Rows.Count; i++)
                    {
                        int roll = Int32.Parse(markGrid.Rows[i].Cells[0].Value.ToString());
                        MarkDistribution md = new MarkDistribution();
                        md.rollNumber = roll;
                        md.subjectCode = subject;
                        md.year = year;
                        md.classCode = clsCode;
                        md.examCode = exam;
                        md.classSection = sec;
                        bool x = md.check();
                        if (x)
                        {
                            DataTable d = md.getMarks();
                            markGrid.Rows[i].Cells[5].Value = d.Rows[0][0].ToString();
                        }
                        else
                        {
                            markGrid.Rows[i].Cells[5].Value = "0";
                        }
                    }
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

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            if (markGrid.Rows.Count == 0)
            {
                Error objx = new Error("No Data To Be Updated");
                objx.ShowDialog();
            }
            else
            {
                MarkDistribution md = new MarkDistribution();
                md.subjectCode = subject;
                md.year = year;
                md.classCode = clsCode;
                md.examCode = exam;
                md.classSection = sec;
                md.markingDate = DateTime.Now.ToShortDateString();
                Subject s = new Subject();
                s.subjectCode = subject;
                DataTable t = s.getTeacher();
                md.teacherCode = t.Rows[0][0].ToString();

                DataTable dsub = s.getAllSubjectsByKey();

                double totalMarks = double.Parse(dsub.Rows[0][2].ToString());
                int loc = -1;
                for (int j = 0; j < markGrid.Rows.Count; j++)
                {
                    if (double.Parse(markGrid.Rows[j].Cells[5].Value.ToString()) > totalMarks)
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
                        md.totalMarks = totalMarks;
                        md.subjectObtainedMarks = double.Parse(markGrid.Rows[i].Cells[5].Value.ToString());
                        if (double.Parse(markGrid.Rows[i].Cells[5].Value.ToString()) >= double.Parse(markGrid.Rows[i].Cells[4].Value.ToString()))
                        {
                            md.remarks = "Pass";
                        }
                        else
                        {
                            md.remarks = "Fail";
                        }
                        if (md.check())
                        {
                            md.updateData();
                        }
                        else
                        {
                            md.addData();
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
