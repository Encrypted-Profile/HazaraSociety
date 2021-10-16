
using System;
using System.Data;
using System.Data.SqlClient;
namespace HazaraSociety.App_Code
{
    class Subject
    {
        public string subjectCode { get; set; }
        public string subjectName { get; set; }
        public double subjectMarks { get; set; }
        public double subjectPassingMarks { get; set; }
        public string subjectClass { get; set; }
        public string teacherCode { get; set; }


        DBConnection strcon = new DBConnection();
        //-------------
        public DataTable getAllSubjects()
        {
            try
            {
                SqlConnection connection = strcon.connect();
                using (SqlDataAdapter ds1 = new SqlDataAdapter("SELECT * from subject ", connection))
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
                throw new Exception("Could not get subject data");
            }
        }
        public DataTable getAllSubjectsByClass()
        {
            try
            {
                SqlConnection connection = strcon.connect();
                using (SqlDataAdapter ds1 = new SqlDataAdapter("SELECT * from subject where subjectClassCode='" + this.subjectClass + "'", connection))
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
                throw new Exception("Could not get subject data");
            }
        }
        public DataTable getAllSubjectsByKey()
        {
            try
            {
                SqlConnection connection = strcon.connect();
                using (SqlDataAdapter ds1 = new SqlDataAdapter("SELECT * from subject where subjectCode='" + this.subjectCode + "'", connection))
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
                throw new Exception("Could not get subject data");
            }
        }
        //-------------
        public DataTable getSubjectsByClass()
        {
            try
            {
                SqlConnection connection = strcon.connect();
                using (SqlDataAdapter ds1 = new SqlDataAdapter("SELECT * from subject where subjectClassCode='" + this.subjectClass + "'", connection))
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
        public DataTable getTeacher()
        {
            try
            {
                SqlConnection connection = strcon.connect();
                using (SqlDataAdapter ds1 = new SqlDataAdapter("SELECT teacherCode from subject where subjectCode='" + this.subjectCode + "'", connection))
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
        //------------------------------
        public bool checkSubject()
        {
            SqlConnection connection = strcon.connect();
            SqlCommand cmd1 = new SqlCommand();
            cmd1.CommandText = "SELECT COUNT(*) from subject where subjectCode='" + this.subjectCode + "'";
            cmd1.CommandType = CommandType.Text;
            cmd1.Connection = connection;
            Int32 count = (Int32)cmd1.ExecuteScalar();
            connection.Close();
            if (count > 0)
                return true;
            else
                return false;
        }
        public void delete()
        {
            try
            {
                SqlConnection connection = strcon.connect();
                string sql = "Delete from subject Where subjectCode='" + this.subjectCode + "'";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Could not delete subject data");
            }
        }
        public void update()
        {
            try
            {
                SqlConnection connection = strcon.connect();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "update subject set teacherCode=@teacher, subjectName=@name,subjectMarks=@marks,subjectPassingMarks=@pMarks,subjectClassCode=@class where subjectCode='" + this.subjectCode + "'";
                cmd.Parameters.AddWithValue("@teacher", this.teacherCode);
                cmd.Parameters.AddWithValue("@name", this.subjectName);
                cmd.Parameters.AddWithValue("@marks", this.subjectMarks);
                cmd.Parameters.AddWithValue("@pMarks", this.subjectPassingMarks);
                cmd.Parameters.AddWithValue("@class", this.subjectClass);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating data");
            }
        }
        public void addNewSubject()
        {
            //insert
            try
            {
                SqlConnection connection = strcon.connect();
                string sql = "INSERT INTO subject(subjectCode,subjectName,subjectMarks,subjectPassingMarks,subjectClassCode,teacherCode)"
                + "VALUES(@code,@name,@marks,@passingMarks,@class,@teacherCode)";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@code", this.subjectCode);
                cmd.Parameters.AddWithValue("@name", this.subjectName);
                cmd.Parameters.AddWithValue("@marks", this.subjectMarks);
                cmd.Parameters.AddWithValue("@passingMarks", this.subjectPassingMarks);
                cmd.Parameters.AddWithValue("@class", this.subjectClass);
                cmd.Parameters.AddWithValue("@teacherCode", this.teacherCode);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Could not insert subject data");
            }
        }
    }
}
