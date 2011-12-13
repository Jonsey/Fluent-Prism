
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Regions;

namespace Prism.Extensions.FluentNH
{
	public abstract class FluentModule<TModule> : FluentDataOnlyModule<TModule>
	{
		#region Fields

	    protected IRegionManager regionManager;

		#endregion Fields

		#region Constructors

        protected FluentModule(IUnityContainer container, IRegionManager manager)
            : base(container)
		{
			this.container = container;
			regionManager = manager;
		}

		#endregion Constructors

		#region Protected Methods

	    protected abstract void RegisterViewsWithRegions();

		#endregion Protected Methods

		#region Public Methods

        public override void Initialize()
        {
            base.Initialize();

            RegisterViewsWithRegions();
        }

	    #endregion Public Methods
	}
}