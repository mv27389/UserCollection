using AutoFixture;

namespace UserCollection.Services.Impl.UnitTest.Helpers
{
	public class ValidUserObjectCreator : ICustomization
	{
		string _firstName;
		string _lastName;
		string _email;
		string _phone;
		bool _active;
		public ValidUserObjectCreator(string firstName, string lastName, string email, string phone, bool active)
		{
			_firstName = firstName;
			_lastName = lastName;
			_email = email;
			_phone = phone;
			_active = active;
		}
		public void Customize(IFixture fixture)
		{
			fixture.Customize<Entities.User>(user => user.With(u => u.FirstName, _firstName).With(u => u.LastName, _lastName)
				.With(u => u.Email, _email).With(u => u.Phone, _phone).With(u => u.Active, _active));
		}
	}
}
