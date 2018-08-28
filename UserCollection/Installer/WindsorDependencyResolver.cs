using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using Castle.Windsor;

namespace UserCollection.Installer
{
	public sealed class WindsorDependencyResolver : IDependencyResolver
	{
		private readonly IWindsorContainer _container;

		public WindsorDependencyResolver(IWindsorContainer container)
		{
			_container = container;
		}

		IDependencyScope IDependencyResolver.BeginScope()
		{
			return new WindsorDependencyScope(_container);
		}

		void IDisposable.Dispose()
		{
			throw new NotImplementedException();
		}

		object IDependencyScope.GetService(Type serviceType)
		{
			return _container.Kernel.HasComponent(serviceType) ? _container.Resolve(serviceType) : null;
		}

		IEnumerable<object> IDependencyScope.GetServices(Type serviceType)
		{
			return _container.ResolveAll(serviceType).Cast<object>().ToArray();

		}
	}
}