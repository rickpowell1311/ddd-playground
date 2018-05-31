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
        private readonly string connectionString;

        public NPocoPersistenceAutofacModule()
        {
        }

        public NPocoPersistenceAutofacModule(string connectionString)
        {
            this.connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new TypeLoadException("Cannot initilize type IDatabase as the connection string name was not passed to NPoco container");
            }

            builder.Register(c => new Database(connectionString, DatabaseType.SQLite, SqliteFactory.Instance))
                .As<IDatabase>()
                .OnActivated(args =>
                {
                    args.Instance.BeginTransaction();
                })
                .OnRelease(d => d.CompleteTransaction())
                .InstancePerLifetimeScope();

            builder.RegisterType<UserAggregateRepository>().As<IUserAggregateRepository>().InstancePerLifetimeScope();
        }
    }
}
