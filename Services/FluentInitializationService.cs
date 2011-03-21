using Microsoft.Practices.Unity;

using Prism.Extensions.FluentNH.Services.interfaces;
using Naimad.Infrastructure.Db;
using NHibernate;

namespace Prism.Extensions.FluentNH.Services
{


    public class FluentInitializationService
    {
        readonly IUnityContainer container;
        readonly string connectionString;

        public FluentInitializationService(IUnityContainer container, string connectionString)
        {
            this.container = container;
            this.connectionString = connectionString;
        }

        public void InitializeModule<T>()
        {
            AddNHibernateMaps<T>();
            RegisterSessionFactory();

            container.RegisterType<IUnitOfWork, SqlUnitOfWork>();
        }
        private void AddNHibernateMaps<T>()
        {
            var dataMappingService = container.Resolve<IMappingRegistryService>();
            if (dataMappingService != null) dataMappingService.AddMapsFromAssemblyOf<T>();
        }

        private void RegisterSessionFactory()
        {
            var SessionFactory = Session.CreateSessionFactory(container, connectionString, true);
            container.RegisterInstance(SessionFactory, new ContainerControlledLifetimeManager());
            container.RegisterInstance(SessionFactory.OpenSession(), new ContainerControlledLifetimeManager());
        }
    }
}
