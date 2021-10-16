using System.Windows.Forms;

namespace HazaraSociety
{
    public partial class StudentManagement : UserControl
    {
        private static StudentManagement _instance;
        public static StudentManagement Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new StudentManagement();
                }
                return _instance;
            }
        }
        public StudentManagement()
        {
            InitializeComponent();
        }

        private void bunifuCustomLabel1_Click(object sender, System.EventArgs e)
        {
            AddClass obj = new AddClass();
            obj.ShowDialog();
        }

        private void bunifuCustomLabel4_Click(object sender, System.EventArgs e)
        {
            AddTeacher obj = new AddTeacher();
            obj.ShowDialog();
        }

        private void manage(object sender, System.EventArgs e)
        {
            ManageTeachers obj = new ManageTeachers();
            obj.ShowDialog();
        }

        private void bunifuCustomLabel2_Click(object sender, System.EventArgs e)
        {
            cboClass obj = new cboClass();
            obj.ShowDialog();
        }
    }
}
