using System.Data.Sql;
using System.Data.SqlClient;

namespace MoodJournal
{
    public class DataSource
    {
        public SqlConnection Connection;

        public SqlConnection LoadConnection()
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
                    return thisConn;
                }
            }
            return thisConn;
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

        public SqlDataReader Read(SqlCommand command)
        {
            if (Connection.State == System.Data.ConnectionState.Closed)
            {
                Connection.Open();

            }

            if (Connection.State == System.Data.ConnectionState.Open)
            {
                command.Connection = Connection;
                SqlDataReader data = command.ExecuteReader();
                Connection.Close();
                return data;
            }
            else
            {
                return null;
            }
        }
    }
}
