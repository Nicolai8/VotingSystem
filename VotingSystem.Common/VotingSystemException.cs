using System;

namespace VotingSystem.Common
{
	public class VotingSystemException : Exception
	{
		public VotingSystemException()
		{
		}

		public VotingSystemException(string message)
			: base(message)
		{
		}

		public VotingSystemException(string format, params object[] args)
			: base(String.Format(format, args))
		{
		}
	}
}
