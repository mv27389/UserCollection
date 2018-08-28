using System.Linq;
using UserCollection.Contracts;

namespace UserCollection.Services.Impl.Mapping
{
	public static class UserMapping
	{
		public static User Map(UserCollection.Entities.User source)
		{
			return new User
			{
				Id = source.Id,
				FirstName = source.FirstName,
				LastName = source.LastName,
				Phone = source.Phone,
				Email = source.Email,
				Active = source.Active
			};
		}

		public static IQueryable<User> Map(IQueryable<UserCollection.Entities.User> source)
		{
			return from src in source
				   select Map(src);
		}

		public static UserCollection.Entities.User Map(User source)
		{
			return new UserCollection.Entities.User
			{
				Id = source.Id,
				FirstName = source.FirstName,
				LastName = source.LastName,
				Phone = source.Phone,
				Email = source.Email,
				Active = source.Active
			};
		}

		public static IQueryable<UserCollection.Entities.User> Map(IQueryable<User> source)
		{
			return from src in source
				   select Map(src);
		}
	}
}
