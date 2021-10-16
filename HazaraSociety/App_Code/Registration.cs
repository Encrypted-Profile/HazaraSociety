using System;
using System.Data;
using System.Data.SqlClient;
namespace HazaraSociety.App_Code
{
    class Registration
    {
        public int id { get; set; }
        public int rollNumber { get; set; }
        public string classCode { get; set; }
        public string className { get; set; }
        public string sectionName { get; set; }
        public int year { get; set; }
        public string status { get; set; }
        public string resultStatus { get; set; }
        DBConnection strcon = new DBConnection();

        //------------
        public DataTable getStudentsForMarkingPosition(int year, string examCode)
        {
            try
            {
                SqlConnection connection = strcon.connect();
                using (SqlDataAdapter ds1 = new SqlDataAdapter("SELECT  s.rollNumber as [Roll Number], s.name as [Name], s.fatherName as [Father Name],(select sum(subjectObtainedMarks) from marks where rollNumber=s.rollNumber AND examCode='" + examCode + "' AND year='" + year + "') as Marks,a.position as [Position] from students s,registration r,attendance a where s.rollNumber=r.rollNumber AND r.classCode='" + this.classCode + "' AND r.sectionId='" + this.sectionName + "' AND r.year='" + year + "' AND s.rollNumber=a.rollNumber AND a.year='" + year + "' AND a.examCode='" + examCode + "' order by Marks DESC", connection))
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
                throw new Exception("Could not get attendance data" + ex);
            }
        }
        public DataTable getStudentsForMarking()
        {
            try
            {
                SqlConnection connection = strcon.connect();
                using (SqlDataAdapter ds1 = new SqlDataAdapter("SELECT  s.rollNumber as [Roll Number], s.name as [Name], s.fatherName as [Father Name] from students s,registration r where s.rollNumber=r.rollNumber AND r.classCode='" + this.classCode + "' AND r.sectionName='" + this.sectionName + "' AND r.year='" + this.year + "'", connection))
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
                throw new Exception("Could not get category data");
            }
        }
        public DataTable getStudentByRollNumberYear()
        {
            try
            {
                SqlConnection connection = strcon.connect();
                using (SqlDataAdapter ds1 = new SqlDataAdapter("SELECT  * from registration where rollNumber='" + rollNumber + "' AND year='" + year + "'", connection))
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
                throw new Exception("Could not get category data");
            }
        }
        //---------------
        public void deleteOldClass()
        {
            try
            {
                SqlConnection connection = strcon.connect();
                string sql = "Delete from registration Where rollNumber='" + this.rollNumber + "' AND classCode='" + this.classCode + "' AND className='" + this.className + "' AND year='" + this.year + "'";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Could not delete class data");
            }
        }
        public void upgrade()
        {
            try
            {
                SqlConnection connection = strcon.connect();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "update registration set status=@status, resultStatus=@rStatus where rollNumber='" + this.rollNumber + "' AND classCode='" + this.classCode + "' AND className='" + this.className + "' AND year='" + this.year + "'";
                cmd.Parameters.AddWithValue("@status", this.status);
                cmd.Parameters.AddWithValue("@rStatus", this.resultStatus);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating data");
            }
        }
        public void register()
        {
            //insert
            try
            {
                SqlConnection connection = strcon.connect();
                string sql = "INSERT INTO registration(rollNumber,classCode,className,sectionName,year,status,resultStatus)"
                + "VALUES(@rollNumber,@classCode,@className,@sectionName,@year,@status,@resultStatus)";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@rollNumber", this.rollNumber);
                cmd.Parameters.AddWithValue("@classCode", this.classCode);
                cmd.Parameters.AddWithValue("@className", this.className);
                cmd.Parameters.AddWithValue("@sectionName", this.sectionName);
                cmd.Parameters.AddWithValue("@year", this.year);
                cmd.Parameters.AddWithValue("@status", this.status);
                cmd.Parameters.AddWithValue("@resultStatus", this.resultStatus);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Could not insert registration data");
            }
        }
        public DataTable getActiveClassByRollNumber()
        {
            try
            {
                SqlConnection connection = strcon.connect();
                using (SqlDataAdapter ds1 = new SqlDataAdapter("SELECT  classCode as [Code],className as [Name],sectionName as [Section],year as [Year] from registration where rollNumber='" + this.rollNumber + "' AND status='Active' AND resultStatus='Not Cleared'", connection))
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
                throw new Exception("Could not get category data");
            }
        }
    }
}
