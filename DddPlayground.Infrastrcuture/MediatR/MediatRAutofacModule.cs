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
              .Register<ServiceFactory>(ctx => {
                  var c = ctx.Resolve<IComponentContext>();
                  return t => c.Resolve(t);
              })
              .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(this.scannedAssemblies).AsImplementedInterfaces();
        }
    }
}
