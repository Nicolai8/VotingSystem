using System;
using log4net;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace VotingSystem.Common
{
	public static class Logger
	{
		private readonly static ILog Log;

		static Logger()
		{
			log4net.Config.XmlConfigurator.Configure();
			Log = LogManager.GetLogger("default");
		}

		public static void Warn(string message)
		{
			Log.Warn(message);
		}

		public static void Error(string message)
		{
			Log.Error(message);
		}

		public static void Error(string message, Exception exception)
		{
			Log.Error(message, exception);
		}

		public static void Info(string message)
		{
			Log.Info(message);
		}
	}
}
