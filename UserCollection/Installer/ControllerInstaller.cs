using System.Web.Http;
using System.Web.Http.Controllers;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using UserCollection.Core.Logging;
using UserCollection.Core.Logging.Impl;
using UserCollection.Core.Repository;
using UserCollection.Core.Repository.Nhibernate;
using UserCollection.Services;
using UserCollection.Services.Impl;

namespace UserCollection.Installer
{
	public class ControllerInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(Types.FromThisAssembly().BasedOn<IHttpController>().If(t => t.Name.EndsWith("Controller")).LifestyleTransient());
			container.Register(Component.For<IUserService>().ImplementedBy<UserService>());
			container.Register(Component.For<IRepository>().ImplementedBy<Repository>());
			container.Register(Component.For<ILogging>().ImplementedBy<Log4NetLogger>());
		}
	}
}