using System;
using System.Diagnostics.Contracts;
using System.Linq;
using UserCollection.Contracts;
using UserCollection.Core.Repository;
using UserCollection.Services.Impl.Mapping;

namespace UserCollection.Services.Impl
{
	public class UserService : IUserService, IServiceImplementation
	{

		private readonly IRepository _repository;

		public UserService(IRepository repository)
		{
			Contract.Requires(repository != null);

			_repository = repository;
		}
		public void ActivateUser(Guid userId)
		{
			Entities.User user = _repository.Read<Entities.User>(userId);

			if (user == default(Entities.User))
			{
				throw new InvalidOperationException("User doesn't exist for id: " + userId);
			}

			user.Active = true;
			_repository.Persist<Entities.User>(user);
		}

		public User CreateUser(User user)
		{
			Entities.User userEntity = UserMapping.Map(user);
			user.Id = Guid.NewGuid();
			_repository.Persist<Entities.User>(userEntity);

			User result = UserMapping.Map(userEntity);

			return result;
		}

		public void DeactiviateUser(Guid userId)
		{
			Entities.User user = _repository.Read<Entities.User>(userId);

			if (user == default(Entities.User))
			{
				throw new InvalidOperationException("User doesn't exist for id: " + userId);
			}

			user.Active = false;
			_repository.Persist<Entities.User>(user);
		}

		public void DeleteUser(Guid userId)
		{
			Entities.User user = _repository.Read<Entities.User>(userId);

			if (user == default(Entities.User))
			{
				throw new InvalidOperationException("User doesn't exist for id: " + userId);
			}

			_repository.Delete<Entities.User>(user);
		}

		public IQueryable<User> GetUsers()
		{
			IQueryable< Entities.User> userEntities = _repository.Query<Entities.User>();

			IQueryable<User> users = UserMapping.Map(userEntities);

			return users;
		}

		public User UpdateUser(User user)
		{
			Entities.User userEntity = _repository.Read<Entities.User>(user.Id);

			if (userEntity == default(Entities.User))
			{
				throw new InvalidOperationException("User doesn't exist for id: " + user.Id);
			}

			userEntity.FirstName = string.IsNullOrWhiteSpace(user.FirstName) ? userEntity.FirstName : user.FirstName;
			userEntity.LastName = string.IsNullOrWhiteSpace(user.LastName) ? userEntity.LastName: user.LastName;
			userEntity.Phone = string.IsNullOrWhiteSpace(user.Phone) ? userEntity.Phone : user.Phone;
			userEntity.Email = string.IsNullOrWhiteSpace(user.Email) ? userEntity.Email: user.Email;

			_repository.Persist<Entities.User>(userEntity);

			return UserMapping.Map(userEntity);
		}
	}
}
