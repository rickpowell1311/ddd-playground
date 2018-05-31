using System;
using System.Data;

namespace DddPlayground.Database.MigrationTools
{
    public interface IDbManager
    {
        Func<IDbConnection> Database { get; }
    }
}
