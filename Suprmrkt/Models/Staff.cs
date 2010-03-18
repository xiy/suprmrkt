using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Suprmrkt.Models
{
	[ORMTable("Staff")]
	class Staff
	{
		public int MaxSpeed { get; set; }
		public int Speed { get; set; }
		public StaffExperience Experience { get; set; }
	}

	public enum StaffExperience 
	{
		Experienced,
		Trainee
	}
}
