using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
//using NHibernate;


namespace UserCollection.Core.Repository.Nhibernate.Installer
{
	public sealed class NhibernateInstaller: IWindsorInstaller
	{
		//private Uri _mappingXmlFilesLocation;

		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			//_mappingXmlFilesLocation = container.Resolve<Uri>("nhibernate.mappingpath");

			//container.Register(Component
			//	.For<ISessionFactory>()
			//	.UsingFactoryMethod(() =>
			//	{
			//		if (_mappingXmlFilesLocation == null)
			//			throw new ArgumentException("Failed to resolve _mappingXmlFilesLocation.", "_mappingXmlFilesLocation");

			//		return new Lazy<ISessionFactory>(BuildSessionFactory, true)
			//	})
			//	.LifeStyle.Singleton
			//);

			//container.Register(Component
			//	.For<IRepository>()
			//	.ImplementedBy<Repository>()
			//);
		}
	}
}
