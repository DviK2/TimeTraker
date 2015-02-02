using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using TimerTracker.DataAcess.Models;

namespace TimerTracker.DataAcess
{
    public sealed class DbRepositoriy
    {
        #region Fields

        DbWrapper _dbWrapper;

        #endregion

        #region Constructor  
        public DbRepositoriy()
        {
            _dbWrapper = new DbWrapper("datbase");
            if (!_dbWrapper.IsExist)
                _dbWrapper.CreateDb();
        }

        #endregion

        #region Methods

        public void InsertWorkHouers(WorkHouers workHouers)
        {
            using (var connection = new SQLiteConnection(_dbWrapper.ConnectionString))
            {
                connection.Open();
                var command = new SQLiteCommand(Queries.InserWorkHouers, connection);
                command.Parameters.AddWithValue("@Date", DateTime.Now);
                command.Parameters.AddWithValue("@Houers", workHouers.Housers);
                command.Parameters.AddWithValue("@Minutes", workHouers.Minutes);
                command.Parameters.AddWithValue("@ProjectId", workHouers.ProjectID);
                command.ExecuteNonQuery();

            };

        }
        
        public  void InserPoject(Project project)
        {
            using (var connection = new SQLiteConnection(_dbWrapper.ConnectionString))
            {
                connection.Open();
                var command = new SQLiteCommand(Queries.InsertProject, connection);
                command.Parameters.AddWithValue("@Name", project.ProjectName);
                command.Parameters.AddWithValue("@Customer", project.Customer);
                command.ExecuteNonQuery();

            };
           
        }

        public IEnumerable<Project> GetProjects()
        {
            var projects = new List<Project>();
            using (var connection = new SQLiteConnection(_dbWrapper.ConnectionString))
            {
                connection.Open();
                var command = new SQLiteCommand(Queries.GetProjects, connection);
                var reader  = command.ExecuteReader();
                while (reader.Read())
                {
                    projects.Add(new Project
                    {
                         Id = reader.GetInt32(0),
                         ProjectName = reader.GetString(1),
                         Customer = reader.GetString(2),
                         Current = reader.GetBoolean(3)
                    });
                }
               

            };
            return projects;
        }

        public void SetCurrentProject(string projectName)
        {
            using (var connection = new SQLiteConnection(_dbWrapper.ConnectionString))
            {
                connection.Open();
                var command = new SQLiteCommand(Queries.SetCurrentProject, connection);
                command.Parameters.AddWithValue("@Name", projectName);
                command.ExecuteNonQuery();

            };
        }

        #endregion


    }
}
