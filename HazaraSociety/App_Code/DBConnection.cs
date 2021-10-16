using System.Data.SqlClient;

namespace HazaraSociety.App_Code
{
    class DBConnection
    {
        SqlConnection connection;
        public DBConnection()
        {
            connection = new SqlConnection(@"data source=(local)\SQLEXPRESS;database=D:\PROJJECTS\HAZARA SOCIETY\HAZARASOCIETY\HAZARASOCIETY\ACADEMY.MDF; integrated security = SSPI");
            //connection = new SqlConnection(@"Data Source=192.168.8.100\SQLEXPRESS;Initial Catalog=D:\InventoryDB\Inventory.MDF;User ID=user@hazarasociety;Password=P@ssw0rd@softnat");
        }
        public SqlConnection connect()
        {
            connection.Open();

            return connection;
        }
        public void disconnect()
        {
            connection.Close();
        }
    }
}
