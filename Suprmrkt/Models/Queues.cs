﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Suprmrkt.Controllers;
using System.Data.SQLite;
using System.Data;

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

        // entry time and everything have to be set!
        public Queues(StaffType type)
        {
			SQLiteResult result = SQLiteController.Instance.Query("SELECT * FROM staff WHERE (type = '" + type.ToString() + "')");
			this.Speed = (int)result.Rows[0]["speed"];
			this.MaxSpeed = (int)result.Rows[0]["speedUp"];
        }

        public enum StaffType
        {
            Experienced,
            Trainee
        }
    }
}
