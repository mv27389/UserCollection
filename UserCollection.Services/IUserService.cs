using System;
using System.Diagnostics.Contracts;
using System.Linq;
using UserCollection.Contracts;

namespace UserCollection.Services
{
	public interface IUserService
    {
		/// <summary>
		/// Gets the list of users.
		/// </summary>
		/// <returns>Returns list of users.</returns>
		IQueryable<User> GetUsers();

		/// <summary>
		/// Create a new user.
		/// </summary>
		/// <param name="user">User contract.</param>
		/// <returns>Return newly created user.</returns>
		User CreateUser(User user);

		/// <summary>
		/// Update existing user fields with the new fields. If field is empty, then doesn't update the field.
		/// </summary>
		/// <param name="user">Updated user contract.</param>
		/// <returns>Return updated user.</returns>
		User UpdateUser(User user);

		/// <summary>
		/// Delete user from database.
		/// </summary>
		/// <param name="userId">User ID.</param>
		void DeleteUser(Guid userId);

		/// <summary>
		/// Activates a user. 
		/// </summary>
		/// <param name="userId">User ID.</param>
		void ActivateUser(Guid userId);

		/// <summary>
		/// Deactivates a user.
		/// </summary>
		/// <param name="userId">User ID.</param>
		void DeactiviateUser(Guid userId);

    }

	abstract class UserServiceClass : IUserService
	{
		void IUserService.ActivateUser(Guid userId)
		{
			Contract.Requires(userId != Guid.Empty);

			throw new NotImplementedException();
		}

		User IUserService.CreateUser(User user)
		{
			Contract.Requires(user != null);

			throw new NotImplementedException();
		}

		void IUserService.DeactiviateUser(Guid userId)
		{
			Contract.Requires(userId != Guid.Empty);

			throw new NotImplementedException();
		}

		void IUserService.DeleteUser(Guid userId)
		{
			Contract.Requires(userId != Guid.Empty);

			throw new NotImplementedException();
		}

		IQueryable<User> IUserService.GetUsers()
		{
			throw new NotImplementedException();
		}

		User IUserService.UpdateUser(User user)
		{
			Contract.Requires(user != null);

			throw new NotImplementedException();
		}
	}
}
