using HazaraSociety.App_Code;
using System;
using System.Data;
using System.Windows.Forms;

namespace HazaraSociety
{
    public partial class AddExam : Form
    {
        public AddExam()
        {
            InitializeComponent();
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
        public void loadData()
        {
            Exam obj = new Exam();
            DataTable dt = obj.getExams();
            examGrid.DataSource = dt;
        }
        public void resetData()
        {
            txtName.Text = "";
            txtCode.Text = "";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string code = (string.IsNullOrWhiteSpace(txtCode.Text)) ? "" : txtCode.Text;
                if (code.Length == 0)
                {
                    Error box = new Error("Exam code is required");
                    box.ShowDialog();
                }
                else
                {
                    string name = (string.IsNullOrWhiteSpace(txtName.Text)) ? "Not Provided" : txtName.Text;
                    Exam exa = new Exam();
                    exa.examCode = code;
                    exa.examName = name;

                    bool x = exa.checkCode();
                    if (x)
                    {
                        Error box = new Error("Sorry Try another exam code");
                        box.ShowDialog();
                    }
                    else
                    {
                        exa.addExam();
                        Done d = new Done();
                        d.ShowDialog();
                        resetData();
                        loadData();
                    }
                }
            }
            catch (Exception ex)
            {
                Error box = new Error("Sorry could not submit data");
                box.ShowDialog();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string code = examGrid.SelectedRows[0].Cells[0].Value.ToString();
                Exam exam = new Exam();
                exam.examCode = code;
                YesNo y = new YesNo();
                y.ShowDialog();
                if (Common.yesno == true)
                {
                    exam.delete();
                    Done d = new Done();
                    d.ShowDialog();
                    resetData();
                    loadData();
                }
                else
                {
                    //-----------------
                }

            }
            catch (Exception ex)
            {
                Error box = new Error("Sorry can not delete");
                box.ShowDialog();
            }
        }
    }
}
