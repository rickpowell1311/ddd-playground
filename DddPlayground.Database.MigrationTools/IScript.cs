namespace DddPlayground.Database.MigrationTools
{
    public interface IScript
    {
        string Sql { get; }

        int Id { get; }
    }
}
