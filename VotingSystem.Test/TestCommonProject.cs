using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VotingSystem.Common;

namespace VotingSystem.Test
{
	[TestClass]
	public class TestCommonProject
	{
		[TestMethod]
		public void TestPasswordCreation()
		{
			const string password = "password";
			string hash = SecurityHelper.CreateHash(password);
			bool verify = SecurityHelper.ComparePasswords(password, hash);

			Assert.IsTrue(verify);

			verify = SecurityHelper.ComparePasswords(password + "1", hash);

			Assert.IsFalse(verify);
		}
	}
}
