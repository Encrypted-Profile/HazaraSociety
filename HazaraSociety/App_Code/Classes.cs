
using System;
using System.Data;
using System.Data.SqlClient;
namespace HazaraSociety.App_Code
{
    class Classes
    {
        public string classCode { get; set; }
        public string className { get; set; }
        public int level { get; set; }
        DBConnection strcon = new DBConnection();


        public int getClassLevel()
        {

            SqlConnection connection = strcon.connect();
            SqlCommand cmd1 = new SqlCommand();
            cmd1.CommandText = "SELECT classLevel from classList where classCode='" + this.classCode + "'";
            cmd1.CommandType = CommandType.Text;
            cmd1.Connection = connection;
            int count = Int32.Parse(cmd1.ExecuteScalar().ToString());
            connection.Close();
            return count;

        }
        public void addNewClass()
        {
            //insert
            try
            {
                SqlConnection connection = strcon.connect();
                string sql = "INSERT INTO classList(classCode,className,classLevel)"
                + "VALUES(@code,@name,@level)";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@code", this.classCode);
                cmd.Parameters.AddWithValue("@name", this.className);
                cmd.Parameters.AddWithValue("@level", this.level);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Could not insert class data" + ex);
            }
        }
        public void delete()
        {
            try
            {
                SqlConnection connection = strcon.connect();
                string sql = "Delete from ClassList Where classCode='" + this.classCode + "'";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Could not delete class data");
            }
        }
        public DataTable getAllClasses()
        {
            try
            {
                SqlConnection connection = strcon.connect();
                using (SqlDataAdapter ds1 = new SqlDataAdapter("SELECT  classCode as [Code],className as [Name],classLevel as [Level] from classList order by classLevel", connection))
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
