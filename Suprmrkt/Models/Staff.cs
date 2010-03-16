using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Suprmrkt.Models
{
	class Staff
	{
		public Staff(int maxSpeed, int speed)
		{
			this.MaxSpeed = maxSpeed;
			this.Speed = speed;
		}
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
