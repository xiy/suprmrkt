using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Suprmrkt.Models
{
    class Queues
    {
        // WHEN A STAFF MEMBER IS (RANDOMLY) ASSIGNED TO A QUEUE
		public int Speed { get; set; }
		public int MaxSpeed { get; set; }

        // THESE VALUES ARE FED WITH THE APPROPRIATE DATA FROM THE STAFF TABLE

        public int Customers { get; set; }
        public int Items { get; set; }

		public Staff StaffMember { get; set; }

        // entry time and everything have to be set!
        public Queues()
        {
        }
    }
}
