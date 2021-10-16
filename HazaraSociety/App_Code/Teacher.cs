
using System;
using System.Data;
using System.Data.SqlClient;
namespace HazaraSociety.App_Code
{
    class Teacher
    {
        public string teacherCode { get; set; }
        public string teacherName { get; set; }
        public string gender { get; set; }
        public string mobile { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string qualification { get; set; }
        public string joiningDate { get; set; }
        public string religion { get; set; }
        public string dob { get; set; }
        DBConnection strcon = new DBConnection();


        public bool checkTeacher()
        {
            try
            {
                SqlConnection connection = strcon.connect();
                SqlCommand cmd = new SqlCommand("SELECT count(*) from teacher where teacherCode='" + this.teacherCode + "'", connection);
                int value = Convert.ToInt32(cmd.ExecuteScalar());

                connection.Close();
                if (value > 0)
                    return true;
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Could not check teacher data");
            }
        }
        //---------------
        public void addTeacher()
        {
            //insert
            try
            {
                SqlConnection connection = strcon.connect();
                string sql = "INSERT INTO teacher(teacherCode,teacherName,teacherGender,teacherMobile,teacherAddress,teacherEmail,teacherQualification,teacherJoiningDate,teacherReligion,teacherDob)"
                + "VALUES(@code,@name,@gender,@mobile,@address,@email,@qualification,@joiningDate,@religion,@dob)";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@code", this.teacherCode);
                cmd.Parameters.AddWithValue("@name", this.teacherName);
                cmd.Parameters.AddWithValue("@gender", this.gender);
                cmd.Parameters.AddWithValue("@mobile", this.mobile);
                cmd.Parameters.AddWithValue("@address", this.address);
                cmd.Parameters.AddWithValue("@email", this.email);
                cmd.Parameters.AddWithValue("@qualification", this.qualification);
                cmd.Parameters.AddWithValue("@joiningDate", this.joiningDate);
                cmd.Parameters.AddWithValue("@religion", this.religion);
                cmd.Parameters.AddWithValue("@dob", this.dob);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Could not insert teacher data");
            }
        }
        public DataTable getTeacherByCode()
        {
            try
            {
                SqlConnection connection = strcon.connect();
                using (SqlDataAdapter ds1 = new SqlDataAdapter("SELECT  * from teacher where teacherCode='" + this.teacherCode + "'", connection))
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
                throw new Exception("Could not get teacher data" + ex);
            }
        }
        public void updateTeacher()
        {
            try
            {
                SqlConnection connection = strcon.connect();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "update teacher set teacherCode=@code,teacherName=@name,teacherGender=@gender,teacherMobile=@mobile,teacherAddress=@address,teacherEmail=@email,teacherQualification=@qualification,teacherReligion=@religion where teacherCode='" + this.teacherCode + "'";
                cmd.Parameters.AddWithValue("@code", this.teacherCode);
                cmd.Parameters.AddWithValue("@name", this.teacherName);
                cmd.Parameters.AddWithValue("@gender", this.gender);
                cmd.Parameters.AddWithValue("@mobile", this.mobile);
                cmd.Parameters.AddWithValue("@address", this.address);
                cmd.Parameters.AddWithValue("@email", this.email);
                cmd.Parameters.AddWithValue("@qualification", this.qualification);
                cmd.Parameters.AddWithValue("@religion", this.religion);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating teacher data");
            }
        }
        public DataTable getAllTeachers()
        {
            try
            {
                SqlConnection connection = strcon.connect();
                using (SqlDataAdapter ds1 = new SqlDataAdapter("SELECT  teacherCode as [Code],teacherName as [Name],teacherGender as [Gender],teacherMobile as [Mobile], teacherAddress as [Address], teacherQualification as [Qualification],teacherJoiningDate as [Joining Date], teacherReligion as [Religion], teacherDob as[Date Of Birth] from teacher ", connection))
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
                throw new Exception("Could not get teacher data");
            }
        }
        public void delete()
        {
            try
            {
                SqlConnection connection = strcon.connect();
                string sql = "Delete from teacher Where teacherCode='" + this.teacherCode + "'";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Could not delete teacher data");
            }
        }
    }


}
