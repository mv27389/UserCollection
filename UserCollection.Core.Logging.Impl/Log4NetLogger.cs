using System;
using System.Diagnostics.Contracts;
using log4net;
using log4net.Core;

namespace UserCollection.Core.Logging.Impl
{
	public sealed class Log4NetLogger : ILogging
	{
		public void Debug(string message)
		{
			GetLogger().Debug(message);
		}

		public void Error(string message, Exception exception)
		{
			GetLogger().Error(message, exception);
		}

		public void Error(string message)
		{
			GetLogger().Error(message);
		}

		public void Info(string message)
		{
			GetLogger().Info(message);
		}

		public void Warning(string message)
		{
			GetLogger().Warn(message);
		}

		private ILog GetLogger()
		{
			Contract.Ensures(Contract.Result<ILog>() != null);

			var log = LogManager.GetLogger("UserCollection");
			if (log == null)
				throw new LogException("No logger was available for the application and no default logger was provided.");

			return log;
		}
	}
}
