using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Suprmrkt.Controllers;
using System.Data.SQLite;
using System.Data;

namespace Suprmrkt.Models.Users
{
	class User
	{
		#region Singleton
		// Lazy loading implementation of the singleton pattern
		User() { }
		static readonly User _instance = new User();

		public static User Instance
		{
			get
			{
				return NestedSingleton._instance;
			}
		}

		class NestedSingleton
		{
			static NestedSingleton() { }
			internal static User _instance = new User();
		}
		#endregion

		public string GetPasswordForUser(UserType user)
		{
			SQLiteResult sqlResult = SQLiteController.Instance.Query(
				"SELECT password FROM Users WHERE (username = '" + user.ToString() + "')");
			if (sqlResult.HasRows)
			{

			}
		}
	}

	public enum UserType
	{
		Advanced,
		Standard
	}
}
