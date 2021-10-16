using HazaraSociety.App_Code;
using System;
using System.Windows.Forms;

namespace HazaraSociety
{
    public partial class YesNo : Form
    {
        public YesNo()
        {
            InitializeComponent();
        }
        public YesNo(string str)
        {
            InitializeComponent();
            lblError.Text = str;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Common.yesno = true;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Common.yesno = false;
            this.Close();
        }
    }
}
