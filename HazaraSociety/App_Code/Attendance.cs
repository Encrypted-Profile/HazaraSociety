
using System;
using System.Data;
using System.Data.SqlClient;
namespace HazaraSociety.App_Code
{
    class Attendance
    {
        public int id { get; set; }
        public string examCode { get; set; }
        public int rollNumber { get; set; }
        public int year { get; set; }
        public double attendance { get; set; }
        public string position { get; set; }
        DBConnection strcon = new DBConnection();
        //-----------------------------------
        public bool check()
        {
            SqlConnection connection = strcon.connect();
            SqlCommand cmd1 = new SqlCommand();
            cmd1.CommandText = "SELECT COUNT(*) from attendance where examCode='" + this.examCode + "' AND rollNumber='" + this.rollNumber + "' AND year='" + this.year + "'";
            cmd1.CommandType = CommandType.Text;
            cmd1.Connection = connection;
            Int32 count = (Int32)cmd1.ExecuteScalar();
            connection.Close();
            if (count > 0)
                return true;
            else
                return false;
        }
        public DataTable getMarks()
        {
            try
            {
                SqlConnection connection = strcon.connect();
                using (SqlDataAdapter ds1 = new SqlDataAdapter("SELECT attendance from attendance  where examCode='" + this.examCode + "' AND rollNumber='" + this.rollNumber + "' AND year='" + this.year + "'", connection))
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
        public DataTable getAnnualAttendance()
        {
            try
            {
                SqlConnection connection = strcon.connect();
                using (SqlDataAdapter ds1 = new SqlDataAdapter("SELECT sum(attendance) as Attendance from attendance  where rollNumber='" + this.rollNumber + "' AND year='" + this.year + "' AND examCode not in ('Annual Final Result')", connection))
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
        public void updatePosition()
        {
            try
            {
                SqlConnection connection = strcon.connect();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "update attendance set position=@position where examCode='" + this.examCode + "' AND rollNumber='" + this.rollNumber + "' AND year='" + this.year + "'";
                cmd.Parameters.AddWithValue("@position", this.position);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating position data");
            }
        }
        public void addAttendance()
        {
            //insert
            try
            {
                SqlConnection connection = strcon.connect();
                string sql = "INSERT INTO attendance(examCode,rollNumber,year,attendance)"
                + "VALUES(@examCode,@rollNumber,@year,@attendance)";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@examCode", this.examCode);
                cmd.Parameters.AddWithValue("@rollNumber", this.rollNumber);
                cmd.Parameters.AddWithValue("@year", this.year);
                cmd.Parameters.AddWithValue("@attendance", this.attendance);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Could not insert attendance data");
            }
        }

        public void updateData()
        {
            try
            {
                SqlConnection connection = strcon.connect();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "update attendance set attendance=@attendance where examCode='" + this.examCode + "' AND rollNumber='" + this.rollNumber + "' AND year='" + this.year + "'";
                cmd.Parameters.AddWithValue("@attendance", this.attendance);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating attendance data");
            }
        }
    }
}
