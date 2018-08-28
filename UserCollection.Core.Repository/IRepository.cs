using System;
using System.Linq;

namespace UserCollection.Core.Repository
{
	public interface IRepository
    {
		T Read<T>(Guid entityId)
			where T : IEntity;

		IQueryable<T> Query<T>()
			where T : IEntity;

		void Persist<T>(T entity)
			where T : IEntity;

		void Delete<T>(T entity)
			where T : IEntity;
	}
}
