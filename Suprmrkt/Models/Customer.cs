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
        Random r = new Random();

		public Customer(String type, int minItems, int maxItems)
        {
            this.CustomerType = type;
            this.Shopping = true;
            this.Items = r.Next(minItems, maxItems);
        }
		
		#region Properties
		public int QueueNumber { get; set; }

		public int Items { get; set; }

		public int EntryTime { get; set; }

        public double Patience { get; set; }

        public int Concentration { set; get; }

        public int Dawdling { set; get; }

		public int LeavingTime { get; set; }

		public int timeForEvent { set; get; }

		public bool Shopping { get; set; }

        public int Basket { get; set; }

        public String CustomerType { get; set; }
		#endregion

	}
}
