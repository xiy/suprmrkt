using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Suprmrkt.Controllers;
using System.Data.SQLite;
using System.Data;

namespace Suprmrkt.Models.Users
{
	class Users
	{
		private SQLiteController sqlController;

		public Users(SQLiteController sql)
		{
			this.sqlController = sql;
		}

		public void GetPasswordForUser(UserType user)
		{
			SQLiteCommand sqlCmd = new SQLiteCommand();
			sqlCmd.Connection = sqlController.SQLConnection;
			switch (user)
			{
				case UserType.Advanced:
					sqlCmd.CommandText = "SELECT password FROM users WHERE (username == 'advanced')";
					SQLiteDataReader sqlReader = sqlCmd.ExecuteReader();
					sqlReader.GetString(0);
					break;
				case UserType.Standard:
					break;
				default:
					break;
			}
		}
	}

	public enum UserType
	{
		Advanced,
		Standard
	}
}
