﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Suprmrkt.Interfaces;

namespace Suprmrkt.Controllers
{
	class SimulationController : ControllerBase, IController
	{
		#region Singleton
		// Lazy loading implementation of the singleton pattern
		SimulationController() { }
		static readonly SimulationController _instance = new SimulationController();

		public static SimulationController Instance
		{
			get
			{
				return NestedSingleton._instance;
			}
		}

		class NestedSingleton
		{
			static NestedSingleton() { }
			internal static SimulationController _instance = new SimulationController();
		} 
		#endregion

		#region IController Members


		public IView View
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public IModel Model
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		#endregion
	}
}
