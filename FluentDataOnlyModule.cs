using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using Prism.Extensions.FluentNH.Services;

namespace Prism.Extensions.FluentNH
{
    public abstract class FluentDataOnlyModule<TModule> : IModule
    {
        protected IUnityContainer container;

        protected FluentDataOnlyModule(IUnityContainer container)
		{
			this.container = container;

		}

        void InitializePersistence()
        {
            var fluentService = new FluentInitializationService(container, GetConnectionString());
            fluentService.InitializeModule<TModule>();
        }

        protected abstract string GetConnectionString();
        protected abstract void RegisterTypesAndServices();

        public virtual void Initialize()
        {
            InitializePersistence();

            RegisterTypesAndServices();
        }
    }
}