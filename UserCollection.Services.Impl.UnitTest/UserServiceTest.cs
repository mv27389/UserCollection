using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserCollection.Services.Impl.Mapping;
using UserCollection.Services.Impl.UnitTest.Helpers;

namespace UserCollection.Services.Impl.UnitTest
{
	[TestClass]
	public class UserServiceTest
	{
		private TestHelper _testHelper;
		private Fixture _fixture;
		private IUserService _userService;

		[TestInitialize]
		public void TestInitialize()
		{
			_testHelper = new TestHelper();
			_fixture = new Fixture();
			_testHelper.SetUserData(new List<Entities.User>());
			_userService = _testHelper.GetUserService();
		}

		[TestMethod]
		public void Constructor_WhenValidArguements_ShouldConstructUserService()
		{
			TestHelper testHelper = new TestHelper();
			IUserService userService = testHelper.GetUserService();

			Assert.IsNotNull(userService);
		}

		[TestMethod]
		public void CreateDiscount_WhenProvidedValidData_ShouldCreateNewUser()
		{
			_fixture.Customize(new ValidUserObjectCreator("Person", "One", "person@one.com", "8765432556", true));
			Entities.User userEntity = _fixture.Create<Entities.User>();
			Contracts.User userContract = UserMapping.Map(userEntity);

			Contracts.User user = _userService.CreateUser(userContract);

			Assert.IsNotNull(user);
			Assert.AreEqual(user.FirstName, userEntity.FirstName);
		}

		[TestMethod]
		public void WhenNoUserDefined_ShouldReturnEmptyList()
		{
			_testHelper.SetUserData(new List<Entities.User>());
			var userList = _userService.GetUsers();
			
			Assert.IsNotNull(userList);
			Assert.AreEqual(0, userList.Count());

		}

		[TestMethod]
		public void WhenUsersAreDefined_ShouldReturnUserList()
		{
			_fixture.Customize(new ValidUserObjectCreator("Person", "One", "person@one.com", "8765432556", true));
			Entities.User userOneEntity = _fixture.Create<Entities.User>();

			_fixture.Customize(new ValidUserObjectCreator("Person", "Two", "person@two.com", "8765432535", true));
			Entities.User userTwoEntity = _fixture.Create<Entities.User>();

			List<Entities.User> userList = new List<Entities.User>();
			userList.Add(userOneEntity);
			userList.Add(userTwoEntity);

			_testHelper.SetUserData(userList);
			var users = _userService.GetUsers();

			Assert.IsNotNull(users);
			Assert.AreEqual(2, users.Count());

		}

		[TestMethod]
		public void WhenValidUserIsDeleted_ShouldDeleteUserAndReturnTrue()
		{
			_fixture.Customize(new ValidUserObjectCreator("Person", "One", "person@one.com", "8765432556", true));
			Entities.User userOneEntity = _fixture.Create<Entities.User>();
			//Contracts.User userOneContract = UserMapping.Map(userOneEntity);
			//_userService.CreateUser(userOneContract);

			_fixture.Customize(new ValidUserObjectCreator("Person", "Two", "person@two.com", "8765432535", true));
			Entities.User userTwoEntity = _fixture.Create<Entities.User>();
			//Contracts.User userTwoContract = UserMapping.Map(userTwoEntity);
			//_userService.CreateUser(userTwoContract);

			List<Entities.User> userList = new List<Entities.User>();
			userList.Add(userOneEntity);
			userList.Add(userTwoEntity);

			_testHelper.SetUserData(userList);

			var result = _userService.DeleteUser(userOneEntity.Id);

			
			Assert.AreEqual(true, result);
		}

	}
}
