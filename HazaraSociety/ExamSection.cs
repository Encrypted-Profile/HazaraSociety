using System.Windows.Forms;

namespace HazaraSociety
{
    public partial class ExamSection : UserControl
    {
        private static ExamSection _instance;
        public static ExamSection Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ExamSection();
                }
                return _instance;
            }
        }
        public ExamSection()
        {
            InitializeComponent();
        }

        private void bunifuCustomLabel1_Click(object sender, System.EventArgs e)
        {
            AddSubject obj = new AddSubject();
            obj.ShowDialog();
        }

        private void bunifuCustomLabel2_Click(object sender, System.EventArgs e)
        {
            AddExam obj = new AddExam();
            obj.ShowDialog();
        }

        private void bunifuCustomLabel4_Click(object sender, System.EventArgs e)
        {
            MarkResultcs obj = new MarkResultcs();
            obj.ShowDialog();
        }

        private void mark(object sender, System.EventArgs e)
        {
            MarkResultcs obj = new MarkResultcs();
            obj.ShowDialog();
        }

        private void view1(object sender, System.EventArgs e)
        {
            ResultViewer obj = new ResultViewer();
            obj.ShowDialog();
        }

        private void attendance(object sender, System.EventArgs e)
        {
            MarkAttendance obj = new MarkAttendance();
            obj.ShowDialog();
        }

        private void position(object sender, System.EventArgs e)
        {
            SelectClass obj = new SelectClass();
            obj.ShowDialog();
        }

        private void final(object sender, System.EventArgs e)
        {
            FinalResult obj = new FinalResult();
            obj.ShowDialog();
        }

        private void generate(object sender, System.EventArgs e)
        {
            GenerateFinalResult obj = new GenerateFinalResult();
            obj.ShowDialog();
        }
    }
}
