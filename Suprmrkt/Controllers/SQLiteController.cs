using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Diagnostics;

namespace Suprmrkt.Controllers
{
	public sealed class SQLiteController : IController
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

		//TODO: Move the database name to a settings variable..
		private string dbPath = Path.Combine(Environment.CurrentDirectory, "SQLDB");
		private SQLiteConnection sqlConn;

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

		/// <summary>
		/// Handles an authentication request from the UI, passing
		/// the given details to the DB which are then verified.
		/// </summary>
		/// <returns>True if the user is authenticated, False otherwise.</returns>
		public bool AuthenticationRequested()
		{
			return false;
		}

		#region IController Members

		public Views.IView View
		{
			get;
			set;
		}

		public Models.IModel Model
		{
			get;
			set;
		}

		#endregion
	}
}
