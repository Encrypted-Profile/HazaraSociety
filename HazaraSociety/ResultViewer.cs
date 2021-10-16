using HazaraSociety.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace HazaraSociety
{
    public partial class ResultViewer : Form
    {
        public ResultViewer()
        {
            InitializeComponent();
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
                //Error objx = new Error("Error while loading exam data");
                //objx.ShowDialog();
                MessageBox.Show(ex.ToString());
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

        private void ResultViewer_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int rolllNumber = Int32.Parse((string.IsNullOrWhiteSpace(txtRollNumber.Text)) ? "0" : txtRollNumber.Text);
                if (rolllNumber == 0)
                {
                    Error error = new Error("Sorry roll number can not be empty");
                    error.ShowDialog();
                }
                else
                {
                    int year = Int32.Parse((string.IsNullOrWhiteSpace(txtYear.Text)) ? "0" : txtYear.Text);
                    string exam = ((KeyValuePair<string, string>)cboExams.SelectedItem).Key;

                    this.SingleTermResultTableAdapter.Fill(this.ReportDataset.SingleTermResult, rolllNumber, year, exam);
                    this.SessionAttendanceTableAdapter.Fill(this.ReportDataset.SessionAttendance, exam, rolllNumber, year);
                    this.getRemarksTableAdapter.Fill(this.ReportDataset.getRemarks, rolllNumber, exam, year);

                    this.reportViewer1.RefreshReport();
                }
            }
            catch (Exception ex)
            {
                Error error = new Error("Facing problem displaying report\nContact system admin");
                error.ShowDialog();
                //MessageBox.Show(ex.ToString());
            }
        }
    }
}
