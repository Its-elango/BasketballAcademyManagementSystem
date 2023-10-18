using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BasketballAcademy.Repository
{
    /// <summary>
    /// Base class for managing SqlConnection for database operations.
    /// </summary>

    public class SqlConnectionBase
    {
        protected SqlConnection Connection { get; private set; }

        /// <summary>
        /// Initializes a new instance of the SqlConnectionBase class.
        /// It reads the connection string from the configuration and creates a SqlConnection.
        /// </summary>
        protected SqlConnectionBase()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            Connection = new SqlConnection(connectionString);
        }

        /// <summary>
        /// Opens the database connection if it is not already open.
        /// </summary>
        public void OpenConnection()
        {
            if (Connection.State != System.Data.ConnectionState.Open)
            {
                Connection.Open();
            }
        }

        /// <summary>
        /// Closes the database connection if it is not already closed.
        /// </summary>
        public void CloseConnection()
        {
            if (Connection.State != System.Data.ConnectionState.Closed)
            {
                Connection.Close();
            }
        }
    }

}