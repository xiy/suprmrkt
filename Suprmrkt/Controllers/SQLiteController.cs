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

		private SQLiteConnection _sqlConnection;
		private string dbPath = Path.Combine(Environment.CurrentDirectory, "buyrite.s3db");

		public SQLiteConnection SqlConnection
		{
			get 
			{
				if (this._sqlConnection == null)
					// Connect to the database file.
					SqlConnection = new SQLiteConnection("data source=" + dbPath);
				if (this._sqlConnection.State == ConnectionState.Closed)
					this.Connect();
				return _sqlConnection; 
			}
			set { _sqlConnection = value; }
		}

		/// <summary>
		/// Connect to the database file.
		/// </summary>
		/// <returns>True if successfully connected to the database.</returns>
		public bool Connect()
		{
			// Assign a handler to notify the UI of the database state.
			this._sqlConnection.StateChange += new StateChangeEventHandler(SqlStateChanged);

			// Perform the actual connection.
			this._sqlConnection.Open();
			return this._sqlConnection.State == ConnectionState.Open;
		}

		/// <summary>
		/// Handles state changes in the database.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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
