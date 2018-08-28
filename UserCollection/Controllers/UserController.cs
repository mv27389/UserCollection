using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
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
			_userService = userService;
		}

		[HttpPost]
		[Route("authenticate")]
		public IHttpActionResult Authenticate()
		{

			IHttpActionResult response;
			HttpResponseMessage responseMsg = new HttpResponseMessage();
			bool isUsernamePasswordValid = false;


			string token = CreateToken();
			//return the token
			return Ok<string>(token);

		}

		/// <summary>
		/// Gets the list of users.
		/// </summary>
		/// <returns>Returns list of all users.</returns>
		[HttpGet]
		[Authorize]
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
		[Authorize]
		public UserModel CreateUser(UserModel model)
		{
			//add model validation 
			if (ModelState.IsValid)
			{
				Contracts.User user = UserModelMapping.Map(model);
				user = _userService.CreateUser(user);

				return UserModelMapping.Map(user);

			}
			else
			{
				throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState));
			}

		}

		/// <summary>
		/// Updates a new user.
		/// </summary>
		/// <param name="id">User Id.</param>
		/// <param name="model">User model.</param>
		/// <returns>Returns updated user model.</returns>
		[HttpPut]
		[Authorize]
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
		[Authorize]
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
		[Authorize]
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
		[Authorize]
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


		private string CreateToken()
		{
			//Set issued at date
			DateTime issuedAt = DateTime.UtcNow;
			//set the time when it expires
			DateTime expires = DateTime.UtcNow.AddDays(7);

			//http://stackoverflow.com/questions/18223868/how-to-encrypt-jwt-security-token
			var tokenHandler = new JwtSecurityTokenHandler();

			//create a identity and add claims to the user which we want to log in
			ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
			{
				new Claim(ClaimTypes.Anonymous, "Name" )
			});

			const string sec = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";
			var now = DateTime.UtcNow;
			var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(sec));
			var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);


			//create the jwt
			var token =
				(JwtSecurityToken)
					tokenHandler.CreateJwtSecurityToken(issuer: "http://localhost:50191", audience: "http://localhost:50191",
						subject: claimsIdentity, notBefore: issuedAt, expires: expires, signingCredentials: signingCredentials);
			var tokenString = tokenHandler.WriteToken(token);

			return tokenString;
		}
	}
}
