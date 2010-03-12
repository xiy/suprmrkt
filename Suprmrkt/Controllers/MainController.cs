using System;
using System.Windows.Forms;
using Suprmrkt.Controllers;
using Suprmrkt.Interfaces;
using Suprmrkt.Views;
using Suprmrkt.Helpers;
using Suprmrkt.Models;

namespace Suprmrkt.Controllers
{
	public class MainController : ControllerBase, IController
	{
		#region Singleton
		// Lazy loading implementation of the singleton pattern
		private MainController() { }
		private static readonly MainController _instance = new MainController();

		public static MainController Instance
		{
			get
			{
				return NestedSingleton._instance;
			}
		}

		/// <summary>
		/// Nested static singleton to ensure thread safety.
		/// </summary>
		class NestedSingleton
		{
			static NestedSingleton() { }
			internal static MainController _instance = new MainController();
		} 
		#endregion


		public void ButtonActionHandler(object sender, ButtonActionEventArgs e)
		{
			switch ((MainActions)e.Button.Tag)
			{
				case MainActions.RunSimulation:
					Simulator s = new Simulator();
					s.Run();
					break;
				case MainActions.GetCustomerTypes:
					Customer.Instance.GetCustomerTypes();
					break;
				case MainActions.NewSimulation:
					break;
				case MainActions.LoadSimulation:
					break;
				case MainActions.ViewResults:
					break;
				case MainActions.Logout:
					break;
				case MainActions.Quit:
					break;
				default:
					break;
			}
		}

		#region IController Members

		public IView View
		{
			get;
			set;
		}

		public IModel Model
		{
			get;
			set;
		}

		#endregion
	}
}
