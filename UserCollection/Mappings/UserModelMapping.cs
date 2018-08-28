using System.Collections.Generic;
using System.Linq;
using UserCollection.Models;

namespace UserCollection.Mappings
{
	public static class UserModelMapping
	{
		public static UserModel Map(Contracts.User source)
		{
			return new UserModel
			{
				Id = source.Id,
				FirstName = source.FirstName,
				LastName = source.LastName,
				Email = source.Email,
				Phone = source.Phone,
				Active = source.Active
			};
		}

		public static IEnumerable<UserModel> Map(IEnumerable<Contracts.User> source)
		{
			return from src in source
				   select Map(src);
		}

		public static Contracts.User Map(UserModel source)
		{
			return new Contracts.User
			{
				Id = source.Id,
				FirstName = source.FirstName,
				LastName = source.LastName,
				Email = source.Email,
				Phone = source.Phone,
				Active = source.Active
			};
		}

		public static IEnumerable<Contracts.User> Map(IEnumerable<UserModel> source)
		{
			return from src in source
				   select Map(src);
		}
	}
}