using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UserCollection.Mappings;
using UserCollection.Models;
using UserCollection.Services;
using UserCollection.Services.Impl;

namespace UserCollection.Controllers
{
	[RoutePrefix("api/user")]
	public class UserController : ApiController
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			//_userService = new UserService();
			_userService = userService;
		}

		/// <summary>
		/// Gets the list of users.
		/// </summary>
		/// <returns>Returns list of all users.</returns>
		[HttpGet]
		[Route("list")]
		public IEnumerable<UserModel> GetUserList()
		{
			IEnumerable<Contracts.User> users = _userService.GetUsers().AsEnumerable();
			var userModels = UserModelMapping.Map(users);

			return userModels;
		}

		/// <summary>
		/// Create a new user.
		/// </summary>
		/// <param name="model">User model.</param>
		/// <returns>Returns newly created user model.</returns>
		[HttpPost]
		public UserModel CreateUser(UserModel model)
		{
			//add model validation 
			if(ModelState.IsValid)
			{ 
			Contracts.User user = UserModelMapping.Map(model);
			user = _userService.CreateUser(user);

				return UserModelMapping.Map(user);

			}
			else
			{
				throw new  HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState));
			}

		}

		/// <summary>
		/// Updates a new user.
		/// </summary>
		/// <param name="id">User Id.</param>
		/// <param name="model">User model.</param>
		/// <returns>Returns updated user model.</returns>
		[HttpPut]
		public UserModel UpdateUser(Guid id, UserModel model)
		{
			if (ModelState.IsValid)
			{
				Contracts.User user = UserModelMapping.Map(model);
				user.Id = id;
				user = _userService.UpdateUser(user);

				return UserModelMapping.Map(user);
			}
			else
			{
				throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState));
			}
		}

		/// <summary>
		/// Deletes a user.
		/// </summary>
		/// <param name="id">User Id.</param>
		/// <returns>True, if delete is a successful operation.</returns>
		[HttpDelete]
		public bool DeleteUser(Guid id)
		{
			try
			{
				_userService.DeleteUser(id);

				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		/// <summary>
		/// Set Active property to True.
		/// </summary>
		/// <param name="id">User Id.</param>
		/// <returns>Return True, if Active property is set to True for the user.</returns>
		[Route("activate")]
		[HttpPut]
		public bool ActivateUser(Guid id)
		{
			try
			{
				_userService.ActivateUser(id);

				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		/// <summary>
		/// Set Active property to False.
		/// </summary>
		/// <param name="id">User Id.</param>
		/// <returns>Return True, if Active property is set to False for the user.</returns>
		[Route("deactivate")]
		[HttpPut]
		public bool DeactivateUser(Guid id)
		{
			try
			{
				_userService.DeactiviateUser(id);

				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}
