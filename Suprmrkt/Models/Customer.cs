using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Suprmrkt.Models
{
    class Customer
    {
		public int QueueNumber { get; set; }

        public int Items
        {
            set { Items++; }
            get { return Items; }
        }

        public int Concentration { set; get; }

        public int Dawdling { set; get; }

		public int EntryTime { get; set; }

        public int LeavingTime
        {
            set { LeavingTime = value; }
            get { return LeavingTime; }
        }

        public int timeForEvent { set; get; }

        public bool Shopping
        {
            set { Shopping = false; }
            get { return Shopping; }
        }

        public Customer()
        {
        }
    }
}
