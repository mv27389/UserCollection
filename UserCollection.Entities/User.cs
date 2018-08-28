using System;
using UserCollection.Core.Repository;
namespace UserCollection.Entities
{
	public class User : IEntity
	{
		public virtual Guid Id { get; set; }

		public virtual string FirstName { get; set; }

		public virtual string LastName { get; set; }

		public virtual string Email { get; set; }

		public virtual string Phone { get; set; }

		public virtual bool Active { get; set; }
	}
}
