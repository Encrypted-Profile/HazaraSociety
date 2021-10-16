using System;
using System.Data;
using System.Data.SqlClient;
namespace HazaraSociety.App_Code
{
    class Student
    {
        public int rollNumber { get; set; }
        public string name { get; set; }
        public string fatherName { get; set; }
        public string dob { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string enrollmentDate { get; set; }
        public double enrollmentFee { get; set; }
        public bool status { get; set; }
        public Byte[] pic { get; set; }


        DBConnection strcon = new DBConnection();
        //------------------------------
        public bool checkStudent()
        {
            SqlConnection connection = strcon.connect();
            SqlCommand cmd1 = new SqlCommand();
            cmd1.CommandText = "SELECT COUNT(*) from students where rollNumber='" + this.rollNumber + "'";
            cmd1.CommandType = CommandType.Text;
            cmd1.Connection = connection;
            Int32 count = (Int32)cmd1.ExecuteScalar();
            connection.Close();
            if (count > 0)
                return true;
            else
                return false;
        }
        public void update()
        {
            try
            {
                SqlConnection connection = strcon.connect();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "update students set name=@name,fatherName=@fatherName,dob=@dob,phoneNumber=@phoneNumber,email=@email,address=@address,pic=@pic where rollNumber='" + this.rollNumber + "'";
                cmd.Parameters.AddWithValue("@name", this.name);
                cmd.Parameters.AddWithValue("@fatherName", this.fatherName);
                cmd.Parameters.AddWithValue("@dob", this.dob);
                cmd.Parameters.AddWithValue("@phoneNumber", this.phoneNumber);
                cmd.Parameters.AddWithValue("@email", this.email);
                cmd.Parameters.AddWithValue("@address", this.address);
                cmd.Parameters.AddWithValue("@pic", this.pic);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating data");
            }
        }
        public void activateImage()
        {
            try
            {
                SqlConnection connection = strcon.connect();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "update students set pic=@pic where rollNumber='" + this.rollNumber + "'";
                cmd.Parameters.AddWithValue("@pic", "D:\\Pictures\\" + this.rollNumber + ".jpg");
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating pic data");
            }
        }
        public DataTable getWithClass()
        {
            try
            {
                SqlConnection connection = strcon.connect();
                using (SqlDataAdapter ds1 = new SqlDataAdapter("SELECT  s.rollNumber,s.name,s.fatherName,s.dob,s.phoneNUmber,s.email,s.address,s.status,s.pic,r.classCode,r.className,r.sectionName,r.year,r.status,r.resultStatus from students s,registration r where s.rollNumber=r.rollNumber AND s.rollNumber='" + this.rollNumber + "' AND r.status='Active' AND r.resultStatus='Not Cleared'", connection))
                {
                    DataSet ds = new DataSet();
                    ds1.Fill(ds);
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];
                    connection.Close();
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Could not get student data");
            }
        }
        public DataTable getStudentById()
        {
            try
            {
                SqlConnection connection = strcon.connect();
                using (SqlDataAdapter ds1 = new SqlDataAdapter("SELECT  rollNumber as [Roll Number],name as [Name],fatherName as [Father Name], dob as [DOB],phoneNumber as [Phone Number], email as [Email] from students where rollNumber like '" + this.rollNumber + "%'", connection))
                {
                    DataSet ds = new DataSet();
                    ds1.Fill(ds);
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];
                    connection.Close();
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Could not get student data");
            }
        }
        //------------------------------------
        public void addStudent()
        {
            //insert
            try
            {
                SqlConnection connection = strcon.connect();
                string sql = "INSERT INTO students(rollNumber,name,fatherName,dob,phoneNumber,email,address,enrollmentDate,enrollmentFee,status,pic)"
                + "VALUES(@rollNumber,@name,@fatherName,@dob,@phoneNumber,@email,@address,@enrollmentDate,@enrollmentFee,@status,@pic)";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@rollNumber", this.rollNumber);
                cmd.Parameters.AddWithValue("@name", this.name);
                cmd.Parameters.AddWithValue("@fatherName", this.fatherName);
                cmd.Parameters.AddWithValue("@dob", this.dob);
                cmd.Parameters.AddWithValue("@phoneNumber", this.phoneNumber);
                cmd.Parameters.AddWithValue("@address", this.address);
                cmd.Parameters.AddWithValue("@email", this.email);
                cmd.Parameters.AddWithValue("@enrollmentDate", this.enrollmentDate);
                cmd.Parameters.AddWithValue("@enrollmentFee", this.enrollmentFee);
                cmd.Parameters.AddWithValue("@status", this.status);
                cmd.Parameters.AddWithValue("@pic", this.pic);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Could not insert student data" + ex);
            }
        }
    }
}
