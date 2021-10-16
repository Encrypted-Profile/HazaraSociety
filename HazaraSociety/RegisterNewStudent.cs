using HazaraSociety.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace HazaraSociety
{
    public partial class RegisterNewStudent : UserControl
    {
        private static RegisterNewStudent _instance;
        public static RegisterNewStudent Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RegisterNewStudent();
                }
                return _instance;
            }
        }

        public RegisterNewStudent()
        {
            InitializeComponent();
            try
            {
                loadClass();
                loadSections();
            }
            catch (Exception ex)
            {
                Error error = new Error("Opps please add class first");
                error.ShowDialog();
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
            }
            catch (Exception ex)
            {
                Error objx = new Error("Error while loading class data");
                objx.ShowDialog();
            }

        }
        private void bunifuSwitch1_Click(object sender, System.EventArgs e)
        {
            if (chkPrint.Value == true)
            {
                Properties.Settings.Default.RegistrationForm = "YES";
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.RegistrationForm = "No";
                Properties.Settings.Default.Save();
            }

        }

        private void image(object sender, System.EventArgs e)
        {
            if (chkImage.Value == true)
            {
                Properties.Settings.Default.StudentImage = "YES";
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.StudentImage = "No";
                Properties.Settings.Default.Save();
            }
        }

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }
        private void button2_Click(object sender, System.EventArgs e)
        {
            Byte[] currentPic;
            Byte[] imgbytes;
            try
            {
                string id = (string.IsNullOrWhiteSpace(txtRollNumber.Text)) ? "" : txtRollNumber.Text;
                if (id.Length == 0)
                {
                    Error er = new Error("Roll Number can not be empty");
                    er.ShowDialog();
                }
                else
                {
                    Student std = new Student();
                    std.rollNumber = Int32.Parse(id);
                    bool x = std.checkStudent();
                    if (x)
                    {
                        Error er = new Error("Roll Number already exist\ntry another roll number");
                        er.ShowDialog();
                    }
                    else
                    {
                        string name = (string.IsNullOrWhiteSpace(txtFullName.Text)) ? "" : txtFullName.Text;
                        string fname = (string.IsNullOrWhiteSpace(txtFatherName.Text)) ? "" : txtFatherName.Text;
                        string dob = txtDob.Value.ToShortDateString();
                        string phone = (string.IsNullOrWhiteSpace(txtPhoneNumber.Text)) ? "" : txtPhoneNumber.Text;
                        string email = (string.IsNullOrWhiteSpace(txtEmail.Text)) ? "" : txtEmail.Text;
                        double fee = double.Parse((string.IsNullOrWhiteSpace(txtEnrollmentFee.Text)) ? "0" : txtEnrollmentFee.Text);
                        string clsCode = ((KeyValuePair<string, string>)cbClass.SelectedItem).Key;
                        string cls = ((KeyValuePair<string, string>)cbClass.SelectedItem).Value;
                        string sec = cboSection.SelectedItem.ToString();
                        string address = (string.IsNullOrWhiteSpace(txtAddress.Text)) ? "" : txtAddress.Text;
                        string enrollmentDate = enrollDAte.Value.ToShortDateString();


                        System.Drawing.Image imgx;
                        //Create the image object
                        Image ximgx = pictureBox1.Image;


                        imgbytes = imageToByteArray(ximgx);

                        std.name = name;
                        std.fatherName = fname;
                        std.dob = dob;
                        std.phoneNumber = phone;
                        std.address = address;
                        std.email = email;
                        std.enrollmentFee = fee;
                        std.enrollmentDate = DateTime.Now.ToShortDateString();
                        std.status = true;
                        std.pic = imgbytes;
                        std.addStudent();

                        Registration reg = new Registration();
                        reg.rollNumber = Int32.Parse(id);
                        reg.classCode = clsCode;
                        reg.className = cls;
                        reg.sectionName = sec;
                        reg.status = "Active";
                        reg.resultStatus = "Not Cleared";
                        reg.year = Int32.Parse(DateTime.Now.ToString("yyyy"));

                        reg.register();


                        Done d = new Done();
                        d.ShowDialog();
                        resetData();

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void resetData()
        {
            txtRollNumber.Text = "";
            txtPhoneNumber.Text = "";
            txtFullName.Text = "";
            txtFatherName.Text = "";
            txtEnrollmentFee.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";
        }
        private void cbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadSections();
        }
    }
}
