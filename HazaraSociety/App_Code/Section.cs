
using System;
using System.Data;
using System.Data.SqlClient;
namespace HazaraSociety.App_Code
{
    class Section
    {
        public int sectionId { get; set; }
        public string sectionName { get; set; }
        public string sectionClass { get; set; }
        public string sectionIncharge { get; set; }
        public bool sectionStatus { get; set; }
        public int year { get; set; }

        DBConnection strcon = new DBConnection();


        //--------
        public void update()
        {
            try
            {
                SqlConnection connection = strcon.connect();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "update section set sectionName=@name,sectionClass=@class,sectionIncharge=@incharge,sectionStatus=@status,year=@year where sectionId='" + this.sectionId + "'";
                cmd.Parameters.AddWithValue("@name", this.sectionName);
                cmd.Parameters.AddWithValue("@class", this.sectionClass);
                cmd.Parameters.AddWithValue("@incharge", this.sectionIncharge);
                cmd.Parameters.AddWithValue("@status", this.sectionStatus);
                cmd.Parameters.AddWithValue("@year", this.year);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating teacher data" + ex);
            }
        }
        public void delete()
        {
            try
            {
                SqlConnection connection = strcon.connect();
                string sql = "Delete from section Where sectionId='" + this.sectionId + "'";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Could not delete class data");
            }
        }
        public DataTable getAllActiveClasses()
        {
            try
            {
                SqlConnection connection = strcon.connect();
                using (SqlDataAdapter ds1 = new SqlDataAdapter("SELECT  sectionId as [Id],sectionName as [Name],sectionClass as [Section Class], sectionIncharge as [Incharge], sectionStatus as [Status], year as [Year] from section where sectionStatus='True' ", connection))
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
                throw new Exception("Could not get section data");
            }
        }
        public DataTable getSetionByClass()
        {
            try
            {
                SqlConnection connection = strcon.connect();
                using (SqlDataAdapter ds1 = new SqlDataAdapter("SELECT  sectionId as [Id],sectionName as [Name],sectionClass as [Section Class], sectionIncharge as [Incharge], sectionStatus as [Status], year as [Year] from section where sectionClass='" + this.sectionClass + "' order by sectionName", connection))
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
                throw new Exception("Could not get section data");
            }
        }
        public void addNewSection()
        {
            //insert
            try
            {
                SqlConnection connection = strcon.connect();
                string sql = "INSERT INTO section(sectionName,sectionClass,sectionIncharge,sectionStatus,year)"
                + "VALUES(@name,@class,@incharge,@status,@year)";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@name", this.sectionName);
                cmd.Parameters.AddWithValue("@class", this.sectionClass);
                cmd.Parameters.AddWithValue("@incharge", this.sectionIncharge);
                cmd.Parameters.AddWithValue("@status", this.sectionStatus);
                cmd.Parameters.AddWithValue("@year", this.year);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Could not insert section data" + ex);
            }
        }
    }
}
