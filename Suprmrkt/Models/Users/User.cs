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
			SQLiteCommand sqlCmd = new SQLiteCommand("SELECT password FROM users WHERE (username = '" + user.ToString() + "')");
			sqlCmd.Connection = SQLiteController.Instance.SqlConnection;
			using (SQLiteDataReader sqlReader = sqlCmd.ExecuteReader())
			{
				sqlReader.Read();
				string password = sqlReader.GetString(0);
				sqlReader.Close();
				return password;
			}
		}
	}

	public enum UserType
	{
		Advanced,
		Standard
	}
}
