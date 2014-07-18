using System;
using BCryptHepler = BCrypt.Net.BCrypt;

namespace VotingSystem.Common
{
	public class SecurityHelper
	{
		private const string LocalParameter = "sSZORo3HWk";

		public static string CreateHash(string password)
		{
			string salt = BCryptHepler.GenerateSalt(6);
			return BCryptHepler.HashPassword(String.Concat(password, LocalParameter), salt);
		}

		public static bool ComparePasswords(string password, string hashedPassword)
		{
			return BCryptHepler.Verify(String.Concat(password, LocalParameter), hashedPassword);
		}
	}
}
