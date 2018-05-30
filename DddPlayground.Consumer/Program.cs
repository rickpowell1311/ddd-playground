using Autofac;
using DddPlayground.Consumer.Features;
using DddPlayground.Domain;
using DddPlayground.Infrastrcuture.MediatR;
using DddPlayground.Infrastructure.EventSourcing;
using DddPlayground.Infrastructure.Persistance;
using DddPlayground.InMemory;
using MediatR;
using System.Reflection;
using System.Threading.Tasks;

namespace DddPlayground.Consumer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var scannedAssemblies = new Assembly[]
            {
                typeof(Program).Assembly,
                typeof(User.Aggregate).Assembly
            };
            
            var builder = new ContainerBuilder();
            builder.RegisterModule(new MediatRAutofacModule(scannedAssemblies));
            builder.RegisterModule(new EventSourcingAutofacModule(scannedAssemblies));

            using (var container = builder.Build())
            using (var scope = container.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();
                await mediator.Send(new CreateUser.Request());
            }

            //  var user = new User.Aggregate("Rick Powell");

            //await persistenceScope.BeginTransaction();

            //var repository = new UserAggregateRepository();
            //user = await repository.Insert(user);

            //await persistenceScope.CommitTransaction();
        }
    }
}
