using System;
using System.ComponentModel.DataAnnotations;

namespace UserCollection.Models
{
	public class UserModel
	{
		public Guid Id { get; set; }
		[Required(ErrorMessage ="FirstName is required.")]
		public string FirstName { get; set; }
		[Required(ErrorMessage = "LastName is required.")]
		public string LastName { get; set; }
		[Required(ErrorMessage = "Email is required.")]
		[RegularExpression(@"^[a-zA-Z0-9_\.-]+@([a-zA-Z0-9-]+\.)+[a-zA-Z]{2,6}$", ErrorMessage ="Email is not valid.")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Phone is required.")]
		[RegularExpression(@".{10}$", ErrorMessage ="Phone number should be have 10 digits.")]
		public string Phone { get; set; }

		public bool Active { get; set; }
	}
}