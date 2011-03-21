
using Microsoft.Practices.Unity;

using Prism.Extensions.FluentNH.Services;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace Prism.Extensions.FluentNH
{
	public abstract class FluentModule<TModule> : IModule
	{
		#region Fields

		protected IUnityContainer container;
		protected IRegionManager regionManager;

		#endregion Fields

		#region Constructors

		protected FluentModule(IUnityContainer container, IRegionManager manager)
		{
			this.container = container;
			regionManager = manager;
		}

		#endregion Constructors

		#region Private Methods

		void InitializePersistence()
		{
			var fluentService = new FluentInitializationService(container, GetConnectionString());
			fluentService.InitializeModule<TModule>();
		}

		#endregion Private Methods

		#region Protected Methods

		protected abstract string GetConnectionString();

		protected abstract void RegisterTypesAndServices();

		protected abstract void RegisterViewsWithRegions();

		#endregion Protected Methods

		#region Public Methods

		public void Initialize()
		{
			InitializePersistence();

			RegisterTypesAndServices();
			RegisterViewsWithRegions();
		}

		#endregion Public Methods
	}
}