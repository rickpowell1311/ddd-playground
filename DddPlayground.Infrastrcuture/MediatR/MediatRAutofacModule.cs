using Autofac;
using MediatR;
using System.Collections.Generic;

namespace DddPlayground.Infrastrcuture.MediatR
{
    public class MediatRAutofacModule : Module
    {
        private System.Reflection.Assembly[] scannedAssemblies;

        public MediatRAutofacModule()
        {
            this.scannedAssemblies = new System.Reflection.Assembly[0];
        }

        public MediatRAutofacModule(params System.Reflection.Assembly[] scannedAssemblies)
        {
            this.scannedAssemblies = scannedAssemblies;
        }

        protected override void Load(ContainerBuilder builder)
        {
            // mediator itself
            builder
              .RegisterType<Mediator>()
              .As<IMediator>()
              .InstancePerLifetimeScope();

            // request handlers
            builder
              .Register<SingleInstanceFactory>(ctx => {
                  var c = ctx.Resolve<IComponentContext>();
                  return t => c.TryResolve(t, out var o) ? o : null;
              })
              .InstancePerLifetimeScope();

            // notification handlers
            builder
              .Register<MultiInstanceFactory>(ctx => {
                  var c = ctx.Resolve<IComponentContext>();
                  return t => (IEnumerable<object>)c.Resolve(typeof(IEnumerable<>).MakeGenericType(t));
              })
              .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(this.scannedAssemblies).AsImplementedInterfaces();
        }
    }
}
