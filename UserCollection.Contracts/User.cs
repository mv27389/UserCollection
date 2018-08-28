﻿using System;

namespace UserCollection.Contracts
{
	public class User
    {
		public Guid Id { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Email { get; set; }

		public string Phone { get; set; }

		public bool Active { get; set; }
	}
}
