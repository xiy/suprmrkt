using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;
using Suprmrkt.Controllers;

namespace Suprmrkt.Models
{
    class Customer
    {
		#region Singleton
		// Lazy loading implementation of the singleton pattern
		Customer() { }
		static readonly Customer _instance = new Customer();

		public static Customer Instance
		{
			get
			{
				return NestedSingleton._instance;
			}
		}

		class NestedSingleton
		{
			static NestedSingleton() { }
			internal static Customer _instance = new Customer();
		}
		#endregion

        Random r = new Random();
		
		#region Properties
		public int QueueNumber { get; set; }

		public int Items { get; set; }

		public int Concentration { set; get; }

		public int Dawdling { set; get; }

		public int EntryTime { get; set; }

		public int LeavingTime { get; set; }

		public int timeForEvent { set; get; }

		public bool Shopping { get; set; }

		public float Patience { get; set; } 
		#endregion

        public enum CustomerType
        {
            Quick,
            Family,
            Pro,
            Novice
        }

		internal string[] GetCustomerTypes()
		{
			SQLiteResult result = SQLiteController.Instance.Query("SELECT Type FROM customers");
			List<string> types = new List<string>();
			for (int i = 0; i < result.Rows.Count; i++)
			{
				types.Add(result.Rows[i]["Type"].ToString());
			}
			return types.ToArray();
		}

		internal Customer GetCustomerByType(CustomerType type)
		{
			Customer customer;
			SQLiteResult result = SQLiteController.Instance.Query("SELECT * FROM customers WHERE (type = '" + type.ToString() + "')");
			if (result.HasRows)
			{
				customer = new Customer();
				customer.Items = r.Next(Convert.ToInt32(result.Rows[0]["minItems"]), Convert.ToInt32(result.Rows[0]["maxItems"]));
				customer.Concentration = Convert.ToInt32(result.Rows[0]["concentration"]);
				customer.Dawdling = Convert.ToInt32(result.Rows[0]["dawdling"]);
				customer.Patience = Convert.ToInt32(result.Rows[0]["patience"]);
			}
		}
	}
}
