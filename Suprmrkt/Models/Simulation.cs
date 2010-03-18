using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Suprmrkt.Interfaces;

namespace Suprmrkt.Models
{
	class Simulation : IModel
	{
		public Simulation(string name, DateTime date, bool isNew)
		{
			this.Name = name;
			this.Date = date;
			this.New = isNew;
		}

		/// <summary>
		/// The name of this Simulation.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The date and time that the Simulation simulates.
		/// </summary>
		public DateTime Date { get; set; }

		/// <summary>
		/// Whether the Simulation has been saved or not.
		/// </summary>
		public bool Saved { get; set; }

		/// <summary>
		/// Whether the Simulation is yet to be run for the first time.
		/// </summary>
		public bool New { get; set; }

		#region IModel Members

		public bool ValidatesLengthOf(object thisObject)
		{
			throw new NotImplementedException();
		}

		public bool ValidatesExistenceOf(object thisObject)
		{
			throw new NotImplementedException();
		}

		public bool ValidatesFormatOf(object thisObject)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
