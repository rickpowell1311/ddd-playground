using Dapper.Contrib.Extensions;

namespace DddPlayground.Database.MigrationTools
{
    [Table("DbVersion")]
    public class ExecutedScript
    {
        [ExplicitKey]
        public int Id { get; set; }
    }
}
