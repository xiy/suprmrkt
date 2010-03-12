using System;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using Suprmrkt.Controllers;
using System.Windows.Forms;
using System.Collections.Generic;
using Suprmrkt.Models;

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

		public SQLiteResult Query(string command)
		{
			SQLiteCommand sqlCmd = new SQLiteCommand(command);
			SQLiteResult sqlResult;
			sqlCmd.Connection = this._sqlConnection;
			using (SQLiteDataReader reader = sqlCmd.ExecuteReader())
			{
				sqlResult = new SQLiteResult();
				sqlResult.Analyse(reader);
				reader.Close();
			}
			return sqlResult;
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

	/// <summary>
	/// Represents the result of a query against an SQLite Database
	/// in an easy to read manner, by parsing the SQLiteDataReader that
	/// is returned from an ExecuteReader call.
	/// </summary>
	public class SQLiteResult
	{
		private Dictionary<string, object> _rows;

		/// <summary>
		/// A Dictionary containing the returned rows of a query.
		/// Keys are Column names, Values are column values.
		/// </summary>
		public Dictionary<string, object> Rows
		{
			get
			{
				if (this._rows == null)
					this._rows = new Dictionary<string, object>();
				return _rows;
			}
			set { _rows = value; }
		}

		/// <summary>
		/// A boolean that determines whether the result returned any rows
		/// or not.
		/// </summary>
		public bool HasRows
		{
			get { return this.Rows.Count > 0; }
		}

		/// <summary>
		/// Analyses the SQLiteDataReader passed to it, filling the
		/// Rows collection using the column name and it's respective value
		/// for each row returned.
		/// </summary>
		/// <param name="reader">The SQLiteDataReader object containing the results of a query.</param>
		public void Analyse(SQLiteDataReader reader)
		{
			while (reader.Read())
			{
				if (reader.HasRows)
				{
					for (int i = 0; i < reader.FieldCount; i++)
					{
						this.Rows.Add(reader.GetName(i), reader.GetValue(i));
					}
				}
			}
		}
	}
}
