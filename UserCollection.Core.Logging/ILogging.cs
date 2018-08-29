using System;
using System.Diagnostics.Contracts;

namespace UserCollection.Core.Logging
{
	public interface ILogging
    {
		void Debug(string message);

		void Info(string message);

		void Warning(string message);

		void Error(string message, Exception exception);

		void Error(string message);
    }

	public abstract class LoggingContractClass : ILogging
	{
		public void Debug(string message)
		{
			Contract.Requires(string.IsNullOrEmpty(message) == false);
			throw new NotImplementedException();
		}

		public void Error(string message, Exception exception)
		{
			Contract.Requires(string.IsNullOrEmpty(message) == false);
			throw new NotImplementedException();
		}

		public void Error(string message)
		{
			Contract.Requires(string.IsNullOrEmpty(message) == false);
			throw new NotImplementedException();
		}

		public void Info(string message)
		{
			Contract.Requires(string.IsNullOrEmpty(message) == false);
			throw new NotImplementedException();
		}

		public void Warning(string message)
		{
			Contract.Requires(string.IsNullOrEmpty(message) == false);
			throw new NotImplementedException();
		}
	}
}
