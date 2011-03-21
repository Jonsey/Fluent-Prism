using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Practices.Unity;

using Prism.Extensions.FluentNH.Services;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace Prism.Extensions.FluentNH
{
    public class Session
    {
        public static ISessionFactory CreateTestSessionFactory(string connStr, List<Assembly> assemblies, bool buildSchema)
        {
           var conn = MsSqlConfiguration.MsSql2005.ConnectionString(c => c.Is(connStr));

            var x = Fluently.Configure()
                .Database(conn)
                .Mappings(m => assemblies.ForEach(a => m.FluentMappings.AddFromAssembly(a)))
                .ExposeConfiguration(BuildSchema);

            return x.BuildSessionFactory();
        }

        public static ISessionFactory CreateSessionFactory(IUnityContainer container, string connStr, bool buildSchema)
        {
            var service = container.Resolve<MappingRegistryService>();

            var conn = MsSqlConfiguration.MsSql2005.ConnectionString(c => c.Is(connStr));

            var x = Fluently.Configure()
                .Database(conn)
                .Mappings(m =>
                {
                    service.Types.ForEach(t => m.FluentMappings.Add(t));
                    service.Assemblies.ForEach(a => m.FluentMappings.AddFromAssembly(a));
                });


            if (buildSchema)
                x = x.ExposeConfiguration(BuildSchema);

            return x.BuildSessionFactory();
        }

        private static void BuildSchema(Configuration config)
        {
            var schemaExport = new SchemaUpdate(config);
            schemaExport.Execute(false, true);
        }
    }
}
