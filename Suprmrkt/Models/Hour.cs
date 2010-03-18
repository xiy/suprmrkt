using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Suprmrkt.Controllers;
using System.Data.SQLite;
using System.Data;

namespace Suprmrkt.Models
{
    class Hour
    {
        public Hour(int totalCustomers, int customerTypeCounter)
        {
            this.totalCustomers = totalCustomers;
            this.customerTypeCounter = customerTypeCounter;

        }

        public int totalCustomers { get; set; }
        public int customerTypeCounter { get; set; }
        public int customersWalkedOut { get; set; }
        public int customersLeaving { get; set; }
        public int customerTimeSpentShopping { get; set; }
        public int customerTimeSpentQueueing { get; set; }
        public double customerTotalHappiness { get; set; }


    }
}
