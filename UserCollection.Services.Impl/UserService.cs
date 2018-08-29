using System;
using System.Diagnostics.Contracts;
using System.Linq;
using UserCollection.Contracts;
using UserCollection.Core.Logging;
using UserCollection.Core.Repository;
using UserCollection.Services.Impl.Mapping;

namespace UserCollection.Services.Impl
{
	public class UserService : IUserService, IServiceImplementation
	{

		private readonly IRepository _repository;
		private readonly ILogging _logger;

		public UserService(IRepository repository, ILogging logger)
		{
			Contract.Requires(repository != null);
			Contract.Requires(logger != null);

			_repository = repository;
			_logger = logger;
		}
		public void ActivateUser(Guid userId)
		{
			_logger.Info("START :: UserService.ActivateUser()");
			Entities.User user = _repository.Read<Entities.User>(userId);

			if (user == default(Entities.User))
			{
				_logger.Error("Error :: UserService.ActivateUser()");
				throw new InvalidOperationException("User doesn't exist for id: " + userId);
			}

			user.Active = true;
			_repository.Persist<Entities.User>(user);
			_logger.Info("END :: UserService.ActivateUser()");
		}

		public User CreateUser(User user)
		{
			_logger.Info(" START :: UserService.CreateUser()");
			Entities.User userEntity = UserMapping.Map(user);
			user.Id = Guid.NewGuid();
			_repository.Persist<Entities.User>(userEntity);

			User result = UserMapping.Map(userEntity);

			_logger.Info(" END :: UserService.CreateUser()");
			return result;
		}

		public void DeactiviateUser(Guid userId)
		{
			_logger.Info(" START :: UserService.DeactivateUser()");
			Entities.User user = _repository.Read<Entities.User>(userId);

			if (user == default(Entities.User))
			{
				_logger.Error("ERROR :: UserService.DeactivateUser()");
				throw new InvalidOperationException("User doesn't exist for id: " + userId);
			}

			user.Active = false;
			_repository.Persist<Entities.User>(user);
			_logger.Info(" END :: UserService.DeactivateUser()");
		}

		public void DeleteUser(Guid userId)
		{
			_logger.Info(" START :: UserService.DeleteUser()");
			Entities.User user = _repository.Read<Entities.User>(userId);

			if (user == default(Entities.User))
			{
				_logger.Error("ERROR :: UserService.DeleteUser()");
				throw new InvalidOperationException("User doesn't exist for id: " + userId);
			}

			_repository.Delete<Entities.User>(user);
			_logger.Info(" END :: UserService.DeleteUser()");
		}

		public IQueryable<User> GetUsers()
		{
			_logger.Info("START :: UserService.GetUsers()");
			IQueryable< Entities.User> userEntities = _repository.Query<Entities.User>();

			IQueryable<User> users = UserMapping.Map(userEntities);

			_logger.Info("END :: UserService.GetUsers()");
			return users;
		}

		public User UpdateUser(User user)
		{
			_logger.Info("START :: UserService.UpdateUser()");
			Entities.User userEntity = _repository.Read<Entities.User>(user.Id);

			if (userEntity == default(Entities.User))
			{
				_logger.Info("ERROR :: UserService.UpdateUser()");
				throw new InvalidOperationException("User doesn't exist for id: " + user.Id);
			}

			userEntity.FirstName = string.IsNullOrWhiteSpace(user.FirstName) ? userEntity.FirstName : user.FirstName;
			userEntity.LastName = string.IsNullOrWhiteSpace(user.LastName) ? userEntity.LastName: user.LastName;
			userEntity.Phone = string.IsNullOrWhiteSpace(user.Phone) ? userEntity.Phone : user.Phone;
			userEntity.Email = string.IsNullOrWhiteSpace(user.Email) ? userEntity.Email: user.Email;

			_repository.Persist<Entities.User>(userEntity);

			_logger.Info("END :: UserService.UpdateUser()");
			return UserMapping.Map(userEntity);
		}
	}
}
