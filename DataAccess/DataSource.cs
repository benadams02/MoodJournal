using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace MoodJournal
{
    public class DataSource
    {
        public SqlConnection Connection;

        public void LoadConnection()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.UserID = Server.Settings.Username;
            builder.Password = Server.Settings.Password;
            builder.DataSource = Server.Settings.InstanceName;
            builder.InitialCatalog = Server.Settings.DatabaseName;
            builder.ConnectTimeout = 60;

            SqlConnection thisConn = new SqlConnection(builder.ConnectionString);

            if (thisConn.State == System.Data.ConnectionState.Closed)
            {
                thisConn.Open();
                if (thisConn.State == System.Data.ConnectionState.Open)
                {
                    thisConn.Close();
                    Connection = thisConn;
                }
            }
        }

        public bool ExecuteQuery(SqlCommand command)
        {
            if (Connection.State == System.Data.ConnectionState.Closed)
            {
                Connection.Open();
                
            }

            if (Connection.State == System.Data.ConnectionState.Open)
            {
                command.Connection = Connection;
                var rowsAffected = command.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable Read(SqlCommand command)
        {
            Dictionary<string, object> SqlObject = new Dictionary<string, object>();
            if (Connection.State == System.Data.ConnectionState.Closed)
            {
                Connection.Open();

            }

            if (Connection.State == System.Data.ConnectionState.Open)
            {
                command.Connection = Connection;
                SqlDataReader data = command.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(data);
                data.Close();
                Connection.Close();
                return dt;
            }
            else
            {
                return null;
            }
        }
    }
}
