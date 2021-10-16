using HazaraSociety.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace HazaraSociety
{
    public partial class ManageStudents : UserControl
    {
        private static ManageStudents _instance;
        public static ManageStudents Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ManageStudents();
                }
                return _instance;
            }
        }
        public ManageStudents()
        {
            InitializeComponent();
            this.ActiveControl = txtSearch;
            txtSearch.Select();

            loadClass();
            loadSections();
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
        private void button2_Click(object sender, System.EventArgs e)
        {
            try
            {
                Student obj = new Student();
                obj.rollNumber = Int32.Parse(txtRollNumber.Text);
                obj.name = (string.IsNullOrWhiteSpace(txtName.Text)) ? "" : txtName.Text;
                obj.fatherName = (string.IsNullOrWhiteSpace(txtFatherName.Text)) ? "" : txtFatherName.Text;
                obj.dob = txtDob.Text;
                obj.phoneNumber = (string.IsNullOrWhiteSpace(txtPhoneNumber.Text)) ? "" : txtPhoneNumber.Text;
                obj.email = (string.IsNullOrWhiteSpace(txtEmail.Text)) ? "" : txtEmail.Text;
                obj.address = (string.IsNullOrWhiteSpace(txtAddress.Text)) ? "" : txtAddress.Text;

                System.Drawing.Image imgx;
                //Create the image object
                Image ximgx = pictureBox.Image;


                imgbytes = imageToByteArray(ximgx);

                obj.pic = imgbytes;



                obj.update();

                Done d = new Done();
                d.ShowDialog();
                resetData();

            }
            catch (Exception ex)
            {

            }
        }
        public void resetData()
        {
            txtAddress.Text = "";
            txtDob.Text = "";
            txtEmail.Text = "";
            txtFatherName.Text = "";
            txtName.Text = "";
            txtPhoneNumber.Text = "";
            txtRollNumber.Text = "";
            txtSearch.text = "";

            studentGrid.DataSource = null;


            pictureBox.Image = Image.FromFile(@"D:\Pictures\default.jpg");
        }

        private void txtSearch_KeyUp(object sender, System.EventArgs e)
        {
            try
            {
                string str = txtSearch.text;
                Student std = new Student();
                std.rollNumber = Int32.Parse(str);
                DataTable dt = std.getStudentById();
                studentGrid.DataSource = dt;

            }
            catch (Exception ex)
            {
                Error er = new Error("Try Again");
                er.ShowDialog();
            }
        }
        Byte[] imgbytes;

        private void studentGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int rollNumber = Int32.Parse(studentGrid.SelectedRows[0].Cells[0].Value.ToString());
                Student obj = new Student();
                obj.rollNumber = rollNumber;
                DataTable dt = obj.getWithClass();


                txtRollNumber.Text = dt.Rows[0][0].ToString();
                txtName.Text = dt.Rows[0][1].ToString();
                txtFatherName.Text = dt.Rows[0][2].ToString();
                txtPhoneNumber.Text = dt.Rows[0][4].ToString();
                txtEmail.Text = dt.Rows[0][5].ToString();
                txtAddress.Text = dt.Rows[0][6].ToString();
                string cls = dt.Rows[0][9].ToString();
                string sec = dt.Rows[0][11].ToString();

                txtDob.Text = studentGrid.SelectedRows[0].Cells[3].Value.ToString();

                int c = 0;
                for (int x = 0; x < cbClass.Items.Count; x++)
                {

                    string str = ((KeyValuePair<string, string>)cbClass.Items[x]).Key;
                    if (cls.Equals(str))
                    {
                        cbClass.SelectedIndex = c;
                        break;
                    }
                    c++;
                }

                loadSections();

                c = 0;
                for (int x = 0; x < cboSection.Items.Count; x++)
                {

                    string str = cboSection.Items[x].ToString();
                    if (sec.Equals(str))
                    {
                        cboSection.SelectedIndex = c;
                        break;
                    }
                    c++;
                }

                this.imgbytes = (Byte[])dt.Rows[0][8];
                Image img1 = byteArrayToImage(this.imgbytes);
                pictureBox.Image = img1;
                //pictureBox.Image = Image.FromFile(@"" + img);

            }
            catch (Exception ex)
            {
                Error er = new Error("There might not be a picture");
                er.ShowDialog();
            }
        }
        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int rNumber = Int32.Parse(studentGrid.SelectedRows[0].Cells[0].Value.ToString());
                YesNo obj = new YesNo();
                obj.ShowDialog();
                if (Common.yesno == true)
                {
                    Student std = new Student();
                    std.rollNumber = rNumber;
                    std.activateImage();
                    Done d = new Done();
                    d.ShowDialog();
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                Error er = new Error("There might not be a picture");
                er.ShowDialog();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int rNumber = Int32.Parse(studentGrid.SelectedRows[0].Cells[0].Value.ToString());
                YesNo obj = new YesNo();
                obj.ShowDialog();
                if (Common.yesno == true)
                {
                    ChangeClass cc = new ChangeClass(rNumber);
                    cc.ShowDialog();
                    resetData();
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                Error er = new Error("Problem changing class ");
                er.ShowDialog();
            }
        }
        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog obj = new OpenFileDialog();
            obj.Filter = "Image Files (*.jpg)|*.jpg";
            obj.FilterIndex = 1;
            DialogResult file = obj.ShowDialog();

            if (file == DialogResult.OK)
            {
                //labelpicture.Text = obj.SafeFileName;
                pictureBox.Image = Image.FromFile(obj.FileName);
                this.imgbytes = imageToByteArray(Image.FromFile(obj.FileName));
            }
        }
    }
}
