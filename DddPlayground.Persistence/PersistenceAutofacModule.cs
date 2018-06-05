using Autofac;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace DddPlayground.Persistence
{
    public class PersistenceAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserAggregateRepository>().As<IUserAggregateRepository>().InstancePerLifetimeScope();
        }
    }
}
