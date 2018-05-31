using Autofac;
using DddPlayground.Consumer.Features;
using DddPlayground.Database.Migrations;
using DddPlayground.Database.MigrationTools;
using DddPlayground.Domain;
using DddPlayground.Infrastrcuture.MediatR;
using DddPlayground.Infrastructure.EventSourcing;
using DddPlayground.Infrastructure.Persistance;
using DddPlayground.Persistence.InMemory;
using DddPlayground.Persistence.NPoco;
using MediatR;
using System.Reflection;
using System.Threading.Tasks;

namespace DddPlayground.Consumer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var dbName = "mydb.sqlite";
            var dbFilePath = @".\";
            var dbConnectionString = $"Data Source={dbFilePath}{dbName};";

            // Database migrations
            var dbManager = new DbManager(cfg =>
            {
                cfg.ConfigureScripts(s =>
                {
                    s.ScanForScripts(typeof(AddUserTable).Assembly);
                });
                cfg.SetDatabaseFilePath(new System.IO.DirectoryInfo(dbFilePath), dbName);
            });

            dbManager.Deploy();

            var scannedAssemblies = new Assembly[]
            {
                typeof(Program).Assembly,
                typeof(User.Aggregate).Assembly
            };

            var builder = new ContainerBuilder();
            builder.RegisterModule(new MediatRAutofacModule(scannedAssemblies));
            builder.RegisterModule(new EventSourcingAutofacModule(scannedAssemblies));
            builder.RegisterModule(new NPocoPersistenceAutofacModule(dbConnectionString));

            using (var container = builder.Build())
            using (var scope = container.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();
                await mediator.Send(new CreateUser.Request());
            }
        }
    }
}
