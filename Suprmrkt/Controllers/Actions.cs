using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Suprmrkt.Controllers
{
	public enum LoginActions
	{
		Login,
		Quit
	}

	public enum MainActions
	{
		RunSimulation,
		NewSimulation,
		LoadSimulation,
		ViewResults,
		Logout,
		Quit
	}
}
