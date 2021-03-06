﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Suprmrkt.Controllers;
using System.Data.SQLite;
using System.Data;

namespace Suprmrkt.Models
{
    class Checkout
    {
        // WHEN A STAFF MEMBER IS (RANDOMLY) ASSIGNED TO A QUEUE
		public Staff AssignedStaffMember { get; set; }

        // THESE VALUES ARE FED WITH THE APPROPRIATE DATA FROM THE STAFF TABLE

        public int Customers { get; set; }
        public int Items { get; set; }

        // entry time and everything have to be set!
        public Checkout(String type)
        {
			SQLiteResult result = SQLiteController.Instance.Query("SELECT * FROM staff WHERE (type = '" + type.ToString() + "')");
			this.AssignedStaffMember.Speed = Convert.ToInt32(result.Rows[0]["speed"]);
			this.AssignedStaffMember.MaxSpeed = Convert.ToInt32(result.Rows[0]["speedUp"]);
        }

        public enum StaffType
        {
            Experienced,
            Trainee
        }
    }
}
