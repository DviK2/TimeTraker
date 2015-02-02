using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimerTracker.DataAcess
{
    internal sealed class DbWrapper
    {
        #region constans

        private const String SQLITE = "sqlite";
        private String _dbName;
        private String _connection;

        #endregion

        #region Constructors

        public DbWrapper(string dbName)
        {
            _dbName = dbName;
        }

        #endregion

        #region Methods
        public void CreateDb()
        {
            SQLiteConnection.CreateFile(string.Format("{0}.{1}", _dbName, SQLITE));
            CreateTables();
        }
       
        private void CreateTables()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                var command = new SQLiteCommand(Queries.CreateProjectTable, connection);
                command.ExecuteNonQuery();

                command = new SQLiteCommand(Queries.CreateWorkHouersTable, connection);
                command.ExecuteNonQuery();
            }
        }

        private String CreateConnectionString()
        {
            return string.Format("Data Source = {0}.{1}; Version=3", _dbName, SQLITE);
        }

        private bool IsDbExists(string _dbName)
        {
            return File.Exists(string.Format("{0}.{1}",_dbName, SQLITE));
        }

        #endregion

        #region Properties

        public bool IsExist { get { return IsDbExists(_dbName); } }

        public String ConnectionString
        {
            get { return _connection ?? (_connection = CreateConnectionString()); }
        }

        #endregion
    }
}
