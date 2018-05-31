using System.IO;

namespace DddPlayground.Database.MigrationTools
{
    public static class DbManagerDefaults
    {
        public static string DefaultDbPath
        {
            get
            {
                return Path.Combine(Directory.GetCurrentDirectory(), "database.sqlite");
            }
        }
    }
}
