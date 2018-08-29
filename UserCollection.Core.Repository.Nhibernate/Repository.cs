using System;
using System.Linq;
using NHibernate;
using NHibernate.Linq;

namespace UserCollection.Core.Repository.Nhibernate
{
	public class Repository : IRepository, IDisposable
	{
		private readonly ISession _session;
		public Repository()
		{
			NHibernate.Cfg.Configuration cfg = new NHibernate.Cfg.Configuration();
			cfg.AddAssembly("UserCollection.Entities");
			ISessionFactory sessionFactory = cfg.BuildSessionFactory();
			_session = sessionFactory.OpenSession();

			if (_session == null)
			{
				throw new InvalidOperationException(@"Unable to open session on database connection.");
			}
		}

		public void Delete<T>(T entity) where T : IEntity
		{
			var transaction = _session.BeginTransaction();

			_session.Delete(entity);

			transaction.Commit();
		}

		public void Persist<T>(T entity) where T : IEntity
		{
			var transaction = _session.BeginTransaction();
			_session.Save(entity);
			transaction.Commit();
		}

		public IQueryable<T> Query<T>() where T : IEntity
		{
			var transaction = _session.BeginTransaction();

			var result = _session.Query<T>();

			if (result == null)
			{
				_session.Dispose();
				throw new InvalidOperationException("Session.Query() result cannnot be null");
			}

			transaction.Commit();
			return result;
		}

		public T Read<T>(Guid entityId) where T : IEntity
		{
			var transaction = _session.BeginTransaction();
			var result = _session.Get<T>(entityId);
			transaction.Commit();

			return result;

		}

		public void Dispose()
		{
			_session.Dispose();
		}
	}
}
