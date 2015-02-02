using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimerTracker.DataAcess
{
    internal static class Queries
    {
        public static string CreateWorkHouersTable = @"create table WorkHouers(
                                                        Id integer PRIMARY KEY AUTOINCREMENT NOT NULL,
                                                        Date Date not null,
                                                        Houers int2 not null,
                                                        Minutes int2 not null,
                                                        ProjectId integer not null, 
                                                        FOREIGN KEY (ProjectId) REFERENCES Projects(Id))";
        public static string CreateProjectTable = @"create table Projects(
                                                    Id integer PRIMARY KEY AUTOINCREMENT NOT NULL,
                                                    Name varchar(50) not null,
                                                    Customer varchar(50) null,
                                                    Current bit not null)";

        public static string InsertProject = @"insert into Projects(Name, Customer, Current) 
                                                values( @Name, @Customer, 0)";

        public static string GetProjects = @"select Id, Name, Customer, Current from Projects";

        public static string SetCurrentProject = @"update Projects 
                                                set Current = 1 
                                                where name = @Name";

        public static string InserWorkHouers = @"insert into WorkHouers(Data, Houers, Minutes, ProjectId)
                                                values(@Date, @Houers, @Minutes, @ProjectId)";

    }
}
