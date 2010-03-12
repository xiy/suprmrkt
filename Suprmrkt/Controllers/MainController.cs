using System;
using System.Windows.Forms;
using Suprmrkt.Controllers;
using Suprmrkt.Interfaces;
using Suprmrkt.Views;
using Suprmrkt.Helpers;
using Suprmrkt.Models;
using System.Collections.Generic;

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


		public void ButtonActionHandler(object sender, EventArgs e)
		{
			Control sendingControl = (Control)sender;
			switch ((MainActions)sendingControl.Tag)
			{
				case MainActions.RunSimulation:
					Simulator s = new Simulator();
					s.Run();
					break;
				case MainActions.GetCustomerTypes:
					string[] ctypes = Customer.Instance.GetCustomerTypes();
					ModelChangedEventArgs cm = new ModelChangedEventArgs();
					cm.ActionReference = MainActions.GetCustomerTypes;
					cm.Params.Add("Customer Types", ctypes);
					RaiseModelChange(this, cm);
					return;
				case MainActions.GetStaffTypes:
					SQLiteResult result = SQLiteController.Instance.Query("SELECT Type FROM staff");
					List<string> types = new List<string>();
					for (int i = 0; i < result.Rows.Count; i++)
					{
						types.Add(result.Rows[i]["Type"].ToString());
					}
					string[] stypes = types.ToArray();
					ModelChangedEventArgs sm = new ModelChangedEventArgs();
					sm.ActionReference = MainActions.GetStaffTypes;
					sm.Params.Add("Staff Types", stypes);
					RaiseModelChange(this, sm);
					return;
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
