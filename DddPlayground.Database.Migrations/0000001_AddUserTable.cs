using DddPlayground.Database.MigrationTools;
using System;
using System.Collections.Generic;
using System.Text;

namespace DddPlayground.Database.Migrations
{
    public class AddUserTable : IScript
    {
        public int Id => 1;

        public string Sql => "CREATE TABLE IF NOT EXISTS User(Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT)";
    }
}
