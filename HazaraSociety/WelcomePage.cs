using System.Windows.Forms;

namespace HazaraSociety
{
    public partial class WelcomePage : UserControl
    {
        private static WelcomePage _instance;
        public static WelcomePage Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new WelcomePage();
                }
                return _instance;
            }
        }
        public WelcomePage()
        {
            InitializeComponent();
        }
    }
}
