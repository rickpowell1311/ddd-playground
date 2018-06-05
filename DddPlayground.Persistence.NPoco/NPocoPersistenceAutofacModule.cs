using Autofac;
using Microsoft.Data.Sqlite;
using NPoco;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace DddPlayground.Persistence.NPoco
{
    public class NPocoPersistenceAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserAggregateRepository>().As<IUserAggregateRepository>().InstancePerLifetimeScope();
        }
    }
}
