using System.Collections.Generic;
using System.Linq;
using Moq;
using UserCollection.Core.Logging;
using UserCollection.Core.Repository;

namespace UserCollection.Services.Impl.UnitTest.Helpers
{
	public class TestHelper
	{
		Mock<IRepository> repositoryMoq;
		Mock<ILogging> loggingMoq;

		public TestHelper()
		{
			repositoryMoq = new Mock<IRepository>(MockBehavior.Loose);
			loggingMoq = new Mock<ILogging>(MockBehavior.Loose);
		}

		public IUserService GetUserService()
		{
			return (IUserService)new UserService(repositoryMoq.Object, loggingMoq.Object);
		}

		public void SetUserData(IEnumerable<Entities.User> entities)
		{
			foreach (var entity in entities)
			{
				repositoryMoq.Setup<Entities.User>(x => x.Read<Entities.User>(entity.Id)).Returns((Entities.User)entity);
				repositoryMoq.Setup(x => x.Persist<Entities.User>(entity));
				repositoryMoq.Setup(x => x.Delete<Entities.User>(entity));
			}

			repositoryMoq.Setup(x => x.Query<Entities.User>()).Returns(entities.AsQueryable());
		}
	}
}
