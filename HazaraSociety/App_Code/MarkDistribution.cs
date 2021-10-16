
using System;
using System.Data;
using System.Data.SqlClient;
namespace HazaraSociety.App_Code
{
    class MarkDistribution
    {
        public int id { get; set; }
        public string markingDate { get; set; }
        public string teacherCode { get; set; }
        public int rollNumber { get; set; }
        public string classCode { get; set; }
        public string classSection { get; set; }
        public string examCode { get; set; }
        public string subjectCode { get; set; }
        public double subjectObtainedMarks { get; set; }
        public double totalMarks { get; set; }
        public double percentage { get; set; }
        public string remarks { get; set; }
        public int year { get; set; }
        DBConnection strcon = new DBConnection();
        //---------------------------------
        public bool check()
        {
            SqlConnection connection = strcon.connect();
            SqlCommand cmd1 = new SqlCommand();
            cmd1.CommandText = "SELECT COUNT(*) from marks where classSection='" + this.classSection + "' AND rollNumber='" + this.rollNumber + "' AND classCode='" + this.classCode + "' AND examCode='" + this.examCode + "' AND subjectCode='" + this.subjectCode + "' AND year='" + this.year + "'";
            cmd1.CommandType = CommandType.Text;
            cmd1.Connection = connection;
            Int32 count = (Int32)cmd1.ExecuteScalar();
            connection.Close();
            if (count > 0)
                return true;
            else
                return false;
        }
        //---------------------------------
        public bool checkFinalExam()
        {
            SqlConnection connection = strcon.connect();
            SqlCommand cmd1 = new SqlCommand();
            cmd1.CommandText = "SELECT COUNT(*) from marks where rollNumber='" + this.rollNumber + "' AND examCode='Annual Final Result' AND year='" + this.year + "'";
            cmd1.CommandType = CommandType.Text;
            cmd1.Connection = connection;
            Int32 count = (Int32)cmd1.ExecuteScalar();
            connection.Close();
            if (count > 0)
                return true;
            else
                return false;
        }
        //-------------------------------
        public DataTable getMarks()
        {
            try
            {
                SqlConnection connection = strcon.connect();
                using (SqlDataAdapter ds1 = new SqlDataAdapter("SELECT subjectObtainedMarks from marks where classSection='" + this.classSection + "' AND rollNumber='" + this.rollNumber + "' AND classCode='" + this.classCode + "' AND examCode='" + this.examCode + "' AND subjectCode='" + this.subjectCode + "' AND year='" + this.year + "'", connection))
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
                throw new Exception("Could not get marks");
            }
        }
        //---------------------------------
        public DataTable getSubjectMarks()
        {
            try
            {
                SqlConnection connection = strcon.connect();
                using (SqlDataAdapter ds1 = new SqlDataAdapter("SELECT sum(totalMarks) as totalMarks, sum(subjectObtainedMarks) as subjectObtainedMarks from marks where classSection='" + this.classSection + "' AND rollNumber='" + this.rollNumber + "' AND examCode='" + this.examCode + "' AND classCode='" + this.classCode + "' AND subjectCode='" + this.subjectCode + "' AND year='" + this.year + "'", connection))
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
                throw new Exception("Could not get marks" + ex);
            }
        }
        //-------------------------------
        public void updateData()
        {
            try
            {
                SqlConnection connection = strcon.connect();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "update marks set subjectObtainedMarks=@subjectObtainedMarks,remarks=@remarks where classSection='" + this.classSection + "' AND rollNumber='" + this.rollNumber + "' AND classCode='" + this.classCode + "' AND examCode='" + this.examCode + "' AND subjectCode='" + this.subjectCode + "' AND year='" + this.year + "'";
                cmd.Parameters.AddWithValue("@subjectObtainedMarks", this.subjectObtainedMarks);
                cmd.Parameters.AddWithValue("@remarks", this.remarks);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating mark data");
            }
        }
        //-------------------------------
        public void updateFinal()
        {
            try
            {
                SqlConnection connection = strcon.connect();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "update marks set totalMarks=@totalMarks,subjectObtainedMarks=@subjectObtainedMarks,remarks=@remarks where classSection='" + this.classSection + "' AND rollNumber='" + this.rollNumber + "' AND classCode='" + this.classCode + "' AND examCode='Annual Final Result' AND subjectCode='" + this.subjectCode + "' AND year='" + this.year + "'";
                cmd.Parameters.AddWithValue("@totalMarks", this.totalMarks);
                cmd.Parameters.AddWithValue("@subjectObtainedMarks", this.subjectObtainedMarks);
                cmd.Parameters.AddWithValue("@remarks", this.remarks);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating mark data");
            }
        }
        public void addData()
        {
            //insert
            try
            {
                SqlConnection connection = strcon.connect();
                string sql = "INSERT INTO marks(markingDate,teacherCode,rollNumber,classCode,classSection,examCode,subjectCode,totalMarks,subjectObtainedMarks,remarks,year)"
                + "VALUES(@markingDate,@teacherCode,@rollNumber,@classCode,@classSection,@examCode,@subjectCode,@totalMarks,@subjectObtainedMarks,@remarks,@year)";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@markingDate", this.markingDate);
                cmd.Parameters.AddWithValue("@teacherCode", this.teacherCode);
                cmd.Parameters.AddWithValue("@rollNumber", this.rollNumber);
                cmd.Parameters.AddWithValue("@classCode", this.classCode);
                cmd.Parameters.AddWithValue("@classSection", this.classSection);
                cmd.Parameters.AddWithValue("@examCode", this.examCode);
                cmd.Parameters.AddWithValue("@subjectCode", this.subjectCode);
                cmd.Parameters.AddWithValue("@totalMarks", this.totalMarks);
                cmd.Parameters.AddWithValue("@subjectObtainedMarks", this.subjectObtainedMarks);
                cmd.Parameters.AddWithValue("@remarks", this.remarks);
                cmd.Parameters.AddWithValue("@year", this.year);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Could not insert marks data" + ex);
            }
        }

    }
}
