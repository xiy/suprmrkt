using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Suprmrkt.Models
{
    class Customer
    {
		public int QueueNumber { get; set; }

		public int Items { get; set; }

        public int Concentration { set; get; }

        public int Dawdling { set; get; }

		public int EntryTime { get; set; }

		public int LeavingTime { get; set; }

        public int timeForEvent { set; get; }

		public bool Shopping { get; set; }

        public Customer()
        {
        }
    }
}
