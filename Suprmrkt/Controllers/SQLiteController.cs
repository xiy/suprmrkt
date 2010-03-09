using System;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using Suprmrkt.Controllers;
using System.Windows.Forms;

namespace Suprmrkt.Controllers
{
	public sealed class SQLiteController
	{
		#region Singleton
		// Lazy loading implementation of the singleton pattern
		private SQLiteController() { }
		private static readonly SQLiteController _instance = new SQLiteController();

		public static SQLiteController Instance
		{
			get
			{
				return NestedSingleton._instance;
			}
		}

		/// <summary>
		/// Nested static singleton to ensure thread safety.
		/// </summary>
		class NestedSingleton
		{
			static NestedSingleton() { }
			internal static SQLiteController _instance = new SQLiteController();
		} 
		#endregion

		private SQLiteConnection sqlConn;
		private string dbPath = Path.Combine(Application.ExecutablePath + "buyrite.s3db");

		private void CreateDatabase()
		{
			// Check to see if the database file exists, if not, create it. 
			// SQLite can do this for us.
			SQLiteConnection.CreateFile(dbPath);
		}

		public bool Connect()
		{
			// Connect to the database file.
			sqlConn = new SQLiteConnection("data source=" + dbPath);

			// Assign a handler to notify the UI of the database state.
			sqlConn.StateChange += new StateChangeEventHandler(SqlStateChanged);
			sqlConn.Commit += new SQLiteCommitHandler(sqlConn_Commit);

			// Perform the actuall connection.
			sqlConn.Open();
			return sqlConn.State == ConnectionState.Open;
		}

		void sqlConn_Commit(object sender, CommitEventArgs e)
		{
			Debug.WriteLine("Committing..");
		}

		public void SqlStateChanged(object sender, StateChangeEventArgs e)
		{
			switch (e.CurrentState)
			{
				case ConnectionState.Broken:
					Debug.WriteLine("DB: Broken");
					break;
				case ConnectionState.Closed:
					Debug.WriteLine("DB: Closed");
					break;
				case ConnectionState.Connecting:
					Debug.WriteLine("DB: Fetching");
					break;
				case ConnectionState.Executing:
					Debug.WriteLine("DB: Executing SQL");
					break;
				case ConnectionState.Fetching:
					Debug.WriteLine("DB: Fetching");
					break;
				case ConnectionState.Open:
					Debug.WriteLine("DB: Opened");
					break;
				default:
					Debug.WriteLine("DB: Undertermined");
					break;
			}
		}
	}
}
