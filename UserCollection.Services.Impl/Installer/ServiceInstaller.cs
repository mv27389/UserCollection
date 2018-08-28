using Castle.MicroKernel.Registration;
using UserCollection.Core.Repository;
using UserCollection.Core.Repository.Nhibernate;

namespace UserCollection.Services.Impl.Installer
{
	class ServiceInstaller:IWindsorInstaller
	{
		void IWindsorInstaller.Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
		{
			//container.Register(Types.FromThisAssembly().BasedOn<IServiceImplementation>()
			//	.If(t => t.Name.EndsWith("Service", System.StringComparison.OrdinalIgnoreCase))
			//	.WithServiceDefaultInterfaces()
			//);
			//container.Register(Component.For<IRepository>().ImplementedBy<Repository>().LifestyleTransient());


		}
	}
}
