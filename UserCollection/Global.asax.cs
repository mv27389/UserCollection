using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;
using UserCollection.Installer;

namespace UserCollection
{
	public class WebApiApplication : System.Web.HttpApplication, IContainerAccessor
	{
		private static readonly IWindsorContainer container = new WindsorContainer();


		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			GlobalConfiguration.Configure(WebApiConfig.Register);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

			InstallWindsorContainerInstallers(GlobalConfiguration.Configuration);
		}

		private void InstallWindsorContainerInstallers(HttpConfiguration configuration)
		{
			container.Install(FromAssembly.This());
			container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel, true));
			var dependencyResolver = new WindsorDependencyResolver(container);
			configuration.DependencyResolver = dependencyResolver;

			//container.Register(Component.For<IWindsorContainer>().Instance(container).LifeStyle.Singleton);
			//container.Install(FromAssembly.InDirectory(new AssemblyFilter(HttpRuntime.BinDirectory, "*.dll")));
		}

		public IWindsorContainer Container
		{
			get
			{
				return container;
			}
		}

	}
}
